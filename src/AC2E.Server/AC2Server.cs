using AC2E.Crypto;
using AC2E.Def.Enums;
using AC2E.Def.Structs;
using AC2E.Protocol;
using AC2E.Protocol.Messages;
using AC2E.Protocol.NetBlob;
using AC2E.Protocol.Packet;
using AC2E.Server.Net;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace AC2E.Server {

    internal class AC2Server {

        private static readonly IPEndPoint ANY_ENDPOINT_V6 = new IPEndPoint(IPAddress.IPv6Any, 0);
        private static readonly IPEndPoint ANY_ENDPOINT = new IPEndPoint(IPAddress.Any, 0);
        private static readonly int MAX_CONNECTIONS = 300;

        private static readonly float ACK_INTERVAL = 2.0f;
        private static readonly float TIME_SYNC_INTERVAL = 20.0f;

        private NetInterface netInterface1;
        private NetInterface netInterface2;
        public bool active;

        private readonly byte[] receiveBuffer = new byte[NetPacket.MAX_SIZE];
        private readonly byte[] sendBuffer = new byte[NetPacket.MAX_SIZE];

        private ushort clientCounter = 1;
        private readonly Dictionary<ushort, ClientConnection> clients = new Dictionary<ushort, ClientConnection>();

        private float serverTime => (DateTime.Now.Ticks - Process.GetCurrentProcess().StartTime.Ticks) / TimeSpan.TicksPerSecond;

        private readonly Language[] SUPPORTED_LANGUAGES = new Language[] {
            Language.ENGLISH,
        };

        ~AC2Server() {
            // Always close in order to release system resources
            // (i.e. those that GC is not aware of, such as a socket)
            if (netInterface1 != null || netInterface2 != null) {
                Log.Warning($"Didn't disconnect AC2Server with interfaces {netInterface1}, {netInterface2} before destruction!");
                disconnect();
            }
        }

        public void start(int port = 0) {
            if (netInterface1 != null || netInterface2 != null) {
                disconnect();
            }

            netInterface1 = port != -1 ? new NetInterface(AddressFamily.InterNetwork, port) : null;
            netInterface2 = port != -1 ? new NetInterface(AddressFamily.InterNetwork, netInterface1.port + 1) : null;

            active = true;

            Log.Debug($"Initialized AC2Server.");
        }

        public void disconnect() {
            // TODO: Disconnect and clear all connections

            if (netInterface1 != null) {
                netInterface1.close();
                netInterface1 = null;
            }
            if (netInterface2 != null) {
                netInterface2.close();
                netInterface2 = null;
            }
            active = false;
        }

        public void processReceive() {
            processReceive(netInterface1);
            processReceive(netInterface2);
        }

        private void processReceive(NetInterface netInterface) {
            while (netInterface != null && netInterface.available) {
                EndPoint baseReceiveEndpoint = netInterface.addressFamily == AddressFamily.InterNetworkV6 ? ANY_ENDPOINT_V6 : ANY_ENDPOINT;
                Array.Clear(receiveBuffer, 0, receiveBuffer.Length);
                int receivedBytes = 0;
                try {
                    receivedBytes = netInterface.receiveFrom(receiveBuffer, ref baseReceiveEndpoint);
                } catch (Exception e) {
                    Log.Error(e, "Bad receive.");
                    // TODO: This should probably also disconnect the client, but getting the correct connection may be difficult
                }
                if (receivedBytes <= 0) {
                    continue;
                }

                try {
                    IPEndPoint receiveEndpoint = (IPEndPoint)baseReceiveEndpoint;

                    Log.Debug($"Rcv[{receivedBytes}] from {receiveEndpoint} - {BitConverter.ToString(receiveBuffer, 0, receivedBytes)}.");

                    processData(receiveBuffer, receivedBytes, receiveEndpoint);
                } catch (Exception e) {
                    Log.Error(e, "Exception when reading packet, discarded.");
                }
            }
        }

        internal void processData(byte[] rawData, int dataLen, IPEndPoint receiveEndpoint) {
            using (BinaryReader data = new BinaryReader(new MemoryStream(rawData, 0, dataLen))) {

                try {
                    NetPacket packet = new NetPacket(data);

                    Log.Debug($"RCVD: {packet}");

                    if (packet.logonHeader != null) {
                        Log.Debug($"Logon request: ts {packet.logonHeader.timestamp} acct {packet.logonHeader.account}");
                        ClientConnection client = addClient(receiveEndpoint, packet.logonHeader.account);
                        if (client != null) {
                            sendConnect(client);
                        } else {
                            Log.Warning("Client tried to connect, but the number of active connections is already at the limit.");
                        }
                    } else if (clients.TryGetValue(packet.recipientId, out ClientConnection client)) {
                        if (packet.flags.HasFlag(NetPacket.Flag.DISCONNECT)) {
                            // TODO: Remove client
                            Log.Information($"Client diconnected, id {packet.recipientId}.");
                            return;
                        }
                        if (packet.flags.HasFlag(NetPacket.Flag.ACK)) {
                            client.highestAckedPacketSeq = packet.ackHeader;
                            // TODO: Clear out any stored packets
                        }

                        for (uint i = client.highestReceivedPacketSeq + 1; i < packet.seq; i++) {
                            client.nackedSeqs.Add(i);
                            Log.Warning($"Nacked seq {i}, client id {packet.recipientId}.");
                        }
                        client.highestReceivedPacketSeq = packet.seq;

                        if (packet.connectAckHeader != 0) {
                            if (packet.connectAckHeader == client.connectionAckCookie) {
                                Log.Debug($"Got good connect ack cookie from client id: {packet.recipientId}.");
                                client.connect();
                                client.nextAckTime = serverTime + ACK_INTERVAL;
                                client.nextAckTime = serverTime + TIME_SYNC_INTERVAL;
                                client.enqueueMessage(new WorldNameMsg {
                                    numConnections = (uint)clients.Count,
                                    maxConnections = (uint)MAX_CONNECTIONS,
                                    worldName = "MyWorld",
                                });
                                client.enqueueMessage(new CliDatInterrogationMsg {
                                    regionId = 1,
                                    nameRuleLanguage = Language.ENGLISH,
                                    supportedLanguages = SUPPORTED_LANGUAGES,
                                });
                                flushSend(client);
                            } else {
                                Log.Warning($"Got bad connect ack cookie from client id: {packet.recipientId} - {packet.connectAckHeader} sent, {client.connectionAckCookie} expected.");
                            }
                        }

                        if (packet.flags.HasFlag(NetPacket.Flag.ECHO_REQUEST)) {
                            client.echoRequestedLocalTime = packet.echoRequestHeader.localTime;
                        }

                        if (packet.frags.Count > 0) {
                            foreach (NetBlobFrag frag in packet.frags) {
                                if (frag.fragCount == 1) {
                                    processNetBlob(client, frag);
                                } else {
                                    Log.Error($"Got fragmented packet from client id: {packet.recipientId} - reassembly not implemented yet!");
                                }
                            }
                        }
                    } else {
                        Log.Warning($"Got packet from unknown client id: {packet.recipientId}.");
                    }
                } catch (Exception e) {
                    Log.Error(e, "Error proccessing data.");
                }
            }
        }

        private void processNetBlob(ClientConnection client, NetBlobFrag blob) {
            using (BinaryReader data = new BinaryReader(new MemoryStream(blob.payload))) {

                MessageOpcode opcode = (MessageOpcode)data.ReadUInt32();

                INetMessage genericMsg = null;

                switch (opcode) {
                    case MessageOpcode.CLIDAT_INTERROGATION_RESPONSE_EVENT: {
                            CliDatInterrogationResponseMsg msg = new CliDatInterrogationResponseMsg(data);
                            genericMsg = msg;
                            client.enqueueMessage(new CliDatEndDDDMsg());
                            CharacterIdentity[] characters = new CharacterIdentity[] {
                                new CharacterIdentity {
                                    id = 0x213000000000dd9d,
                                    name = "TestChar",
                                    greyedOutForSeconds = 0,
                                    vDesc = new VisualDesc {
                                        baseSetupId = 0x1F001110,
                                    },
                                },
                            };
                            client.enqueueMessage(new LoginMinCharSetMsg {
                                accountName = client.accountName,
                                characters = characters,
                            });
                            client.enqueueMessage(new LoginCharacterSetMsg {
                                characters = characters,
                                numAllowedCharacters = 10,
                                accountName = client.accountName,
                                hasLegions = true,
                                useTurbineChat = true,
                            });
                            flushSend(client);
                            break;
                        }
                    case MessageOpcode.CHARACTER_CREATE_EVENT: {
                            CharacterCreateMsg msg = new CharacterCreateMsg(data);
                            genericMsg = msg;
                            // TODO: Create character
                            break;
                        }
                    case MessageOpcode.CHARACTER_ENTER_GAME_EVENT: {
                            CharacterEnterGameMsg msg = new CharacterEnterGameMsg(data);
                            genericMsg = msg;
                            client.enqueueMessage(new CreatePlayerMsg {
                                objectId = msg.characterId,
                                regionId = 1,
                            });
                            client.enqueueMessage(new LoginPlayerDescMsg {

                            });
                            client.enqueueMessage(new CreateObjectMsg {
                                objectId = msg.characterId,
                                vDesc = new VisualDesc {
                                    baseSetupId = 0x1F001110,
                                },
                            });
                            flushSend(client);
                            break;
                        }
                    case MessageOpcode.CLIDAT_REQUEST_DATA_EVENT: {
                            CliDatRequestDataMsg msg = new CliDatRequestDataMsg(data);
                            genericMsg = msg;
                            client.enqueueMessage(new CliDatErrorMsg {
                                fileDbType = msg.fileDbType,
                                fileId = msg.fileId,
                                unk1 = 1,
                            });
                            flushSend(client);
                            break;
                        }
                    case MessageOpcode.Evt_Interp__InterpSEvent_ID: {
                            InterpSEventMsg msg = new InterpSEventMsg(data);
                            genericMsg = msg;
                            // TODO: Just for testing - when pressing the attack mode button, go into portal space after a 1sec delay
                            if (msg.funcId == (uint)EventFunctionId.StartAttack) {
                                client.enqueueMessage(new InterpCEventPrivateMsg {
                                    netEvent = new EnterPortalSpaceEvt {
                                        delay = 1,
                                    }
                                });
                            }
                            flushSend(client);
                            break;
                        }
                    default: {
                            Log.Error($"Unhandled opcode: {opcode} - message not processed! Header: {blob}");
                            break;
                        }
                }

                Log.Debug($"Got msg: {genericMsg}");
            }
        }

        private ClientConnection addClient(IPEndPoint clientEndpoint, string accountName) {
            if (clients.Count > MAX_CONNECTIONS) {
                return null;
            }

            ClientConnection client = new ClientConnection(clientCounter, clientEndpoint, accountName);
            clients[clientCounter] = client;
            clientCounter++;
            return client;
        }

        private void sendConnect(ClientConnection client) {
            sendPacket(client, new NetPacket {
                connectHeader = new ConnectHeader {
                    connectionAckCookie = client.connectionAckCookie,
                    recipientId = client.id,
                    outgoingSeed = client.serverSeed,
                    incomingSeed = client.clientSeed,
                },
            });
        }

        private void flushSend(ClientConnection client) {
            while (client.fragQueue.Count > 0) {
                sendPacket(client, new NetPacket());
                Thread.Sleep(50);
            }
        }

        private void sendPacket(ClientConnection client, NetPacket packet) {
            float curServerTime = serverTime;

            packet.seq = client.packetSeq;
            client.packetSeq++;
            packet.recipientId = client.id;
            packet.interval = (ushort)curServerTime;
            // TODO: Need to advance this?
            packet.iteration = 1;

            if (client.connected && serverTime > client.nextAckTime) {
                packet.ackHeader = client.highestReceivedPacketSeq;
                client.nextAckTime = serverTime + ACK_INTERVAL;
            }

            if (client.connected && serverTime > client.nextTimeSyncTime) {
                packet.timeSyncHeader = serverTime;
                client.nextTimeSyncTime = serverTime + TIME_SYNC_INTERVAL;
            }

            if (client.echoRequestedLocalTime != -1.0f) {
                packet.echoResponseHeader = new EchoResponseHeader {
                    localTime = client.echoRequestedLocalTime,
                    localToServerTimeDelta = curServerTime - client.echoRequestedLocalTime,
                };
                client.echoRequestedLocalTime = -1.0f;
            }

            if (client.fragQueue.Count > 0) {
                // TODO: Just assuming that all fragments cause encryption for now - there might be cases where they don't need to or shouldn't
                packet.flags |= NetPacket.Flag.ENCRYPTED_CHECKSUM;
                packet.flags |= NetPacket.Flag.FRAGMENTS;
            }

            using (BinaryWriter data = new BinaryWriter(new MemoryStream(sendBuffer))) {
                // Write header
                packet.writeHeader(data);
                uint headerChecksum = CryptoUtil.calcChecksum(sendBuffer, 0, data.BaseStream.Position, true);

                // Write optional headers
                long contentStart = data.BaseStream.Position;
                uint contentChecksum = 0;
                packet.writeOptionalHeaders(data, sendBuffer, ref contentChecksum);

                // Fill with fragments until full
                if (packet.flags.HasFlag(NetPacket.Flag.FRAGMENTS)) {
                    while (client.fragQueue.TryPeek(out NetBlobFrag frag)) {
                        long lastFragStartPos = data.BaseStream.Position;
                        try {
                            uint fragChecksum = 0;

                            long dataStart = data.BaseStream.Position;
                            frag.writeHeader(data);
                            fragChecksum += CryptoUtil.calcChecksum(sendBuffer, dataStart, data.BaseStream.Position - dataStart, true);

                            dataStart = data.BaseStream.Position;
                            frag.writePayload(data);
                            fragChecksum += CryptoUtil.calcChecksum(sendBuffer, dataStart, data.BaseStream.Position - dataStart, true);

                            packet.frags.Add(frag);
                            client.fragQueue.Dequeue();
                            contentChecksum += fragChecksum;
                        } catch (Exception e) {
                            if (!(e is ArgumentException || e is NotSupportedException)) {
                                Log.Error(e, "Error writing fragment.");
                            }
                            data.BaseStream.Seek(lastFragStartPos, SeekOrigin.Begin);
                            break;
                        }
                    }
                }

                // Encrypt checksum if necessary
                if (packet.flags.HasFlag(NetPacket.Flag.ENCRYPTED_CHECKSUM)) {
                    if (!packet.hasIsaacXor) {
                        packet.isaacXor = client.serverIsaac.Next();
                        packet.hasIsaacXor = true;
                    }
                    contentChecksum ^= packet.isaacXor;
                }

                int packetLength = (int)data.BaseStream.Position;
                ushort contentLength = (ushort)(packetLength - contentStart);

                // Replace the content length and also update the checksum
                BitConverter.GetBytes(contentLength).CopyTo(sendBuffer, 16);
                headerChecksum += contentLength;

                // Replace the checksum
                BitConverter.GetBytes(headerChecksum + contentChecksum).CopyTo(sendBuffer, 8);

                netInterface1.sendTo(sendBuffer, packetLength, client.endpoint);

                Log.Debug($"Send[{packetLength}] to {client.endpoint} - {BitConverter.ToString(sendBuffer, 0, packetLength)}.");
            }

            Log.Debug($"SENT: {packet}");
        }
    }
}
