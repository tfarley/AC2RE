using AC2E.Def;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace AC2E.Server {

    internal class PacketHandler {

        private readonly List<Language> SUPPORTED_LANGUAGES = new List<Language> {
            Language.ENGLISH,
        };

        private readonly AccountManager accountManager;
        private readonly ClientManager clientManager;
        private readonly ServerTime serverTime;

        public PacketHandler(AccountManager accountManager, ClientManager clientManager, ServerTime serverTime) {
            this.accountManager = accountManager;
            this.clientManager = clientManager;
            this.serverTime = serverTime;
        }

        public void processReceive(NetInterface netInterface, byte[] rawData, int dataLen, IPEndPoint receiveEndpoint) {
            // NOTE: This method should complete as quickly as possible so that the executing thread can be returned to the pool used by IOCP
            using (AC2Reader data = new AC2Reader(new MemoryStream(rawData, 0, dataLen))) {
                NetPacket packet = new NetPacket(data);

                Log.Debug($"RCVD [{dataLen}] on {receiveEndpoint} | {packet}\n{BitConverter.ToString(rawData, 0, dataLen)}.");

                if (packet.logonHeader != null) {
                    Log.Debug($"Logon request: seq {packet.logonHeader.netAuth.connectionSeq} acct {packet.logonHeader.netAuth.accountName}");

                    // TODO: Remove, just to create a temporary new account
                    if (!accountManager.accountExistsWithUserName(packet.logonHeader.netAuth.accountName)) {
                        accountManager.register(packet.logonHeader.netAuth.accountName, "");
                    }

                    Account account = accountManager.authenticate(packet.logonHeader.netAuth.accountName, "");

                    clientManager.addClient(netInterface, 0.0f, 0.0f, receiveEndpoint, account);
                } else if (packet.flags.HasFlag(NetPacket.Flag.LOGOFF)) {
                    Log.Information($"Client disconnected, id {packet.recipientId}.");
                    clientManager.removeClient(new ClientId(packet.recipientId));
                } else if (clientManager.tryGetClient(new ClientId(packet.recipientId), out ClientConnection client)) {
                    // TODO: Need to handle client acking the re-sent (nacked) packets
                    if (packet.flags.HasFlag(NetPacket.Flag.PAK)) {
                        client.ackPacket(packet.ackHeader);
                    }

                    if (packet.flags.HasFlag(NetPacket.Flag.NAK)) {
                        foreach (uint seq in packet.nacksHeader) {
                            client.nackPacket(seq);
                        }
                    }

                    if (client.connected && packet.seq <= client.highestReceivedPacketSeq) {
                        if (!packet.flags.HasFlag(NetPacket.Flag.PAK) && !packet.flags.HasFlag(NetPacket.Flag.NAK)) {
                            Log.Warning($"Got dupe packet with seq {packet.seq}, expecting {client.highestReceivedPacketSeq}.");
                        }
                        return;
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
                            client.enqueueMessage(new WorldNameMsg {
                                worldName = new StringInfo { literalValue = "MyWorld" },
                            });
                            client.enqueueMessage(new CliDatInterrogationMsg {
                                regionId = (RegionID)1,
                                nameRuleLanguage = Language.ENGLISH,
                                supportedLanguages = SUPPORTED_LANGUAGES,
                            });
                        } else {
                            Log.Warning($"Got bad connect ack cookie from client id: {packet.recipientId} - {packet.connectAckHeader} sent, {client.connectionAckCookie} expected.");
                        }
                    }

                    if (packet.flags.HasFlag(NetPacket.Flag.ECHO_REQUEST)) {
                        client.echoRequestedLocalTime = packet.echoRequestHeader.localTime;
                        client.echoRequestedServerTime = serverTime.elapsedTime;
                    }

                    if (packet.frags.Count > 0) {
                        foreach (NetBlobFrag frag in packet.frags) {
                            client.addFragment(frag);
                        }
                    }
                } else {
                    Log.Warning($"Got packet from unknown client id: {packet.recipientId}.");
                }
            }
        }

        public void processNetBlobs(World world) {
            lock (clientManager) {
                foreach (ClientConnection client in clientManager.clients) {
                    if (!client.connected) {
                        continue;
                    }

                    if (!world.playerExists(client.id)) {
                        world.addPlayer(client.id, client.account);
                    }

                    while (client.incomingCompleteBlobs.TryDequeue(out NetBlob blob)) {
                        using (AC2Reader data = new AC2Reader(new MemoryStream(blob.payload))) {

                            MessageOpcode opcode = (MessageOpcode)data.ReadUInt32();
                            INetMessage genericMsg = INetMessage.read(opcode, data, true);

                            StringBuilder msgString = new StringBuilder(genericMsg.ToString());
                            if (opcode == MessageOpcode.Evt_Interp__InterpSEvent_ID) {
                                InterpSEventMsg msg = (InterpSEventMsg)genericMsg;
                                msgString.Append($" {msg.netEvent.funcId}");
                            }

                            Log.Debug($"Got msg: {msgString}");

                            if (world.processMessage(client.id, genericMsg) && data.BaseStream.Position < data.BaseStream.Length) {
                                Log.Warning($"NetBlob was not fully read ({data.BaseStream.Position} / {data.BaseStream.Length}).");
                            }
                        }
                    }
                }
            }
        }

        public void send(ClientId clientId, INetMessage message) {
            lock (clientManager) {
                if (clientManager.tryGetClient(clientId, out ClientConnection client)) {
                    client.enqueueMessage(message);
                }
            }
        }
    }
}
