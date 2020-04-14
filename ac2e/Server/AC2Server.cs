using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;

class AC2Server {

    private static readonly IPEndPoint ANY_ENDPOINT_V6 = new IPEndPoint(IPAddress.IPv6Any, 0);
    private static readonly IPEndPoint ANY_ENDPOINT = new IPEndPoint(IPAddress.Any, 0);
    private static readonly int MAX_CONNECTIONS = 300;
    private static readonly int PACKET_BUFFER_SIZE = 1200;

    private static readonly float ACK_INTERVAL = 2.0f;
    private static readonly float TIME_SYNC_INTERVAL = 20.0f;

    private NetInterface netInterface1;
    private NetInterface netInterface2;
    public bool active;

    private readonly byte[] receiveBuffer = new byte[PACKET_BUFFER_SIZE];
    private readonly byte[] sendBuffer = new byte[PACKET_BUFFER_SIZE];

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
            ALog.warn($"Didn't disconnect AC2Server with interfaces {netInterface1}, {netInterface2} before destruction!");
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

        ALog.debug($"Initialized AC2Server.", CatProto.i);
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
                ALog.exception(e);
                // TODO: This should probably also disconnect the client, but getting the correct connection may be difficult
            }
            if (receivedBytes <= 0) {
                continue;
            }

            try {
                IPEndPoint receiveEndpoint = (IPEndPoint)baseReceiveEndpoint;

                ALog.debug($"Rcv[{receivedBytes}] from {receiveEndpoint} - {BitConverter.ToString(receiveBuffer, 0, receivedBytes)}.", CatTransport.i);

                processData(receiveBuffer, receivedBytes, receiveEndpoint);
            } catch (Exception e) {
                ALog.error($"Exception when reading packet, discarded.");
                ALog.exception(e);
            }
        }
    }

    internal void processData(byte[] rawData, int dataLen, IPEndPoint receiveEndpoint) {
        BinaryReader data = new BinaryReader(new MemoryStream(rawData, 0, dataLen));

        try {
            Packet packet = new Packet(data);

            ALog.debug($"RCVD: {packet}");

            if (packet.logonHeader != null) {
                ALog.debug($"Logon request: ts {packet.logonHeader.timestamp} acct {packet.logonHeader.account}");
                ClientConnection client = addClient(receiveEndpoint, packet.logonHeader.account);
                if (client != null) {
                    sendConnect(client);
                } else {
                    ALog.warn("Client tried to connect, but the number of active connections is already at the limit.");
                }
            } else if (clients.TryGetValue(packet.recipientId, out ClientConnection client)) {
                if (packet.flags.HasFlag(Packet.Flag.DISCONNECT)) {
                    // TODO: Remove client
                    ALog.info($"Client diconnected, id {packet.recipientId}.");
                    return;
                }
                if (packet.flags.HasFlag(Packet.Flag.ACK)) {
                    client.highestAckedPacketSeq = packet.ackHeader;
                    // TODO: Clear out any stored packets
                }

                for (uint i = client.highestReceivedPacketSeq + 1; i < packet.seq; i++) {
                    client.nackedSeqs.Add(i);
                    ALog.warn($"Nacked seq {i}, client id {packet.recipientId}.");
                }
                client.highestReceivedPacketSeq = packet.seq;

                if (packet.connectAckHeader != 0) {
                    if (packet.connectAckHeader == client.connectionAckCookie) {
                        ALog.debug($"Got good connect ack cookie from client id: {packet.recipientId}.");
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
                        sendPacket(client, new Packet());
                    } else {
                        ALog.warn($"Got bad connect ack cookie from client id: {packet.recipientId} - {packet.connectAckHeader} sent, {client.connectionAckCookie} expected.");
                    }
                }

                if (packet.flags.HasFlag(Packet.Flag.ECHO_REQUEST)) {
                    client.echoRequestedLocalTime = packet.echoRequestHeader.localTime;
                }

                if (packet.frags.Count > 0) {
                    foreach (NetBlobFrag frag in packet.frags) {
                        if (frag.fragCount == 1) {
                            processNetBlob(client, frag);
                        } else {
                            ALog.error($"Got fragmented packet from client id: {packet.recipientId} - reassembly not implemented yet!");
                        }
                    }
                }
            } else {
                 ALog.warn($"Got packet from unknown client id: {packet.recipientId}.");
            }
        } catch (Exception e) {
            ALog.exception(e);
        }
    }

    private void processNetBlob(ClientConnection client, NetBlobFrag blob) {
        BinaryReader data = new BinaryReader(new MemoryStream(blob.payload));

        MessageOpcode opcode = (MessageOpcode)data.ReadUInt32();

        INetMessage genericMsg = null;

        switch (opcode) {
            case MessageOpcode.CLIDAT_INTERROGATION_RESPONSE_EVENT: {
                    CliDatInterrogationResponseMsg msg = new CliDatInterrogationResponseMsg(data);
                    genericMsg = msg;
                    client.enqueueMessage(new CliDatEndDDDMsg());
                    CharacterIdentity[] characters = new CharacterIdentity[] { };
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
                    sendPacket(client, new Packet());
                    break;
                }
            case MessageOpcode.CHARACTER_CREATE_EVENT: {
                    CharacterCreateMsg msg = new CharacterCreateMsg(data);
                    genericMsg = msg;
                    break;
                }
            default: {
                    ALog.error($"Unhandled opcode: {opcode} - message not processed! Header: {blob}");
                    break;
                }
        }

        ALog.debug($"Got msg: {genericMsg}");
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
        sendPacket(client, new Packet {
            connectHeader = new ConnectHeader {
                connectionAckCookie = client.connectionAckCookie,
                recipientId = client.id,
                outgoingSeed = client.serverSeed,
                incomingSeed = client.clientSeed,
            },
        });
    }

    private void sendPacket(ClientConnection client, Packet packet) {
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
            packet.flags |= Packet.Flag.ENCRYPTED_CHECKSUM;
            packet.flags |= Packet.Flag.FRAGMENTS;
        }

        BinaryWriter data = new BinaryWriter(new MemoryStream(sendBuffer));

        // Write header
        packet.writeHeader(data);
        uint headerChecksum = CryptoUtil.calcChecksum(sendBuffer, 0, data.BaseStream.Position, true);

        // Write optional headers
        long contentStart = data.BaseStream.Position;
        uint contentChecksum = 0;
        packet.writeOptionalHeaders(data, sendBuffer, ref contentChecksum);

        // Fill with fragments until full
        if (packet.flags.HasFlag(Packet.Flag.FRAGMENTS)) {
            while (client.fragQueue.TryPeek(out NetBlobFrag frag)) {
                long lastFragStartPos = data.BaseStream.Position;
                try {
                    long dataStart = data.BaseStream.Position;
                    frag.writeHeader(data);
                    contentChecksum += CryptoUtil.calcChecksum(sendBuffer, dataStart, data.BaseStream.Position - dataStart, true);

                    dataStart = data.BaseStream.Position;
                    frag.writePayload(data);
                    contentChecksum += CryptoUtil.calcChecksum(sendBuffer, dataStart, data.BaseStream.Position - dataStart, true);

                    packet.frags.Add(frag);
                    client.fragQueue.Dequeue();
                } catch (ArgumentException) {
                    data.BaseStream.Seek(lastFragStartPos, SeekOrigin.Begin);
                }
            }
        }

        // Encrypt checksum if necessary
        if (packet.flags.HasFlag(Packet.Flag.ENCRYPTED_CHECKSUM)) {
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

        ALog.debug($"Send[{packetLength}] to {client.endpoint} - {BitConverter.ToString(sendBuffer, 0, packetLength)}.", CatTransport.i);

        ALog.debug($"SENT: {packet}");
    }
}
