using AC2RE.Definitions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace AC2RE.Server {

    internal class PacketHandler {

        private readonly List<Language> SUPPORTED_LANGUAGES = new() {
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
            // NOTE: This method must also be thread safe
            using (AC2Reader data = new(new MemoryStream(rawData, 0, dataLen))) {
                NetPacket packet = new(data);

                Logs.NET.trace("RCVD",
                    "len", dataLen,
                    "endpoint", receiveEndpoint,
                    "pkt", packet,
                    "data", BitConverter.ToString(rawData, 0, dataLen));

                if (!clientManager.tryProcessClient(new(packet.recipientId), client => processDataPacket(receiveEndpoint, client, packet))) {
                    if (!processLogonPacket(netInterface, receiveEndpoint, packet)) {
                        Logs.NET.warn("Got data packet from unknown client",
                            "clientId", packet.recipientId);
                    }
                }
            }
        }

        public bool processLogonPacket(NetInterface netInterface, IPEndPoint receiveEndpoint, NetPacket packet) {
            if (packet.logonHeader == null) {
                return false;
            }

            bool clientExists = false;
            clientManager.processClients(client => {
                // TODO: Should be based on account name, not sequence
                if (client.connectionSeq == packet.logonHeader.netAuth.connectionSeq) {
                    clientExists = true;
                }
            });

            if (clientExists) {
                return true;
            }

            Logs.NET.debug("Logon request",
                "seq", packet.logonHeader.netAuth.connectionSeq,
                "acct", packet.logonHeader.netAuth.accountName);

            // TODO: Remove, just to create a temporary new account
            if (!accountManager.accountExistsWithUserName(packet.logonHeader.netAuth.accountName)) {
                accountManager.create(packet.logonHeader.netAuth.accountName, "");
            }

            Account? account = accountManager.authenticate(packet.logonHeader.netAuth.accountName, "");

            if (account == null) {
                Logs.ACCOUNT.debug("Authentication failed",
                    "seq", packet.logonHeader.netAuth.connectionSeq,
                    "acct", packet.logonHeader.netAuth.accountName);
                throw new ArgumentException($"Authentication failed for account {packet.logonHeader.netAuth.accountName}.");
            }

            ClientId clientId = clientManager.addClient(packet.logonHeader.netAuth.connectionSeq, receiveEndpoint, account);

            clientManager.processClient(clientId, client => {
                client.sendPacket(netInterface, 0.0f, 0.0f, new() {
                    connectHeader = new() {
                        connectionAckCookie = client.connectionAckCookie,
                        netId = client.id.id,
                        outgoingSeed = client.outgoingSeed,
                        incomingSeed = client.incomingSeed,
                    },
                });
            });

            return true;
        }

        public void processDataPacket(IPEndPoint receiveEndpoint, ClientConnection client, NetPacket packet) {
            if (!client.endpoint.Equals(receiveEndpoint)) {
                if (client.endpoint.Address.Equals(receiveEndpoint.Address)) {
                    Logs.NET.debug("Updated client endpoint port to match sender",
                        "client", client,
                        "endpoint", client.endpoint,
                        "receiveEndpoint", receiveEndpoint);
                    client.updatePort(receiveEndpoint.Port);
                } else {
                    Logs.NET.debug("Client does not match sender's endpoint",
                        "client", client,
                        "endpoint", client.endpoint,
                        "receiveEndpoint", receiveEndpoint);
                    return;
                }
            }

            if (packet.flags.HasFlag(NetPacket.Flag.LOGOFF)) {
                Logs.NET.info($"Client disconnected",
                    "client", client);
                clientManager.removeClient(new(packet.recipientId));
                return;
            }

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
                    Logs.NET.debug("Got dupe packet",
                        "client", client,
                        "seq", packet.seq,
                        "expectedSeq", client.highestReceivedPacketSeq);
                }
                return;
            }

            for (uint i = client.highestReceivedPacketSeq + 1; i < packet.seq; i++) {
                client.nackedSeqs.Add(i);
                Logs.NET.debug("Nacked seq",
                    "client", client,
                    "seq", i);
            }
            client.highestReceivedPacketSeq = packet.seq;

            if (packet.connectAckHeader != 0) {
                if (packet.connectAckHeader == client.connectionAckCookie) {
                    Logs.NET.info("Client connected",
                        "client", client);
                    client.connect();
                    send(client.id, new WorldNameMsg {
                        worldName = new("MyWorld"),
                    });
                    send(client.id, new CliDatInterrogationMsg {
                        regionId = (RegionID)1,
                        nameRuleLanguage = Language.ENGLISH,
                        supportedLanguages = SUPPORTED_LANGUAGES,
                    });
                } else {
                    Logs.NET.debug("Got bad connect ack cookie",
                        "client", client,
                        "sentCookie", packet.connectAckHeader,
                        "expectedCookie", client.connectionAckCookie);
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
        }

        public void processNetBlobs(World world) {
            clientManager.processClients(client => {
                if (!client.connected) {
                    return;
                }

                world.addPlayerIfNecessary(client, client.account);

                while (client.incomingCompleteBlobs.TryDequeue(out NetBlob? blob)) {
                    using (AC2Reader data = new(new MemoryStream(blob.payload))) {

                        MessageOpcode opcode = (MessageOpcode)data.ReadUInt32();
                        INetMessage genericMsg = INetMessage.read(opcode, data, true);

                        StringBuilder msgString = new(genericMsg.ToString());
                        if (opcode == MessageOpcode.Evt_Interp__InterpSEvent_ID) {
                            InterpSEventMsg msg = (InterpSEventMsg)genericMsg;
                            msgString.Append($" {msg.netEvent.funcId}");
                        }

                        Logs.NET.debug("Got msg",
                            "client", client,
                            "msg", msgString);

                        if (world.processMessage(client, genericMsg) && data.BaseStream.Position < data.BaseStream.Length) {
                            Logs.NET.warn("NetBlob was not fully read",
                                "client", client,
                                "pos", data.BaseStream.Position,
                                "len", data.BaseStream.Length);
                        }
                    }
                }
            });
        }

        private byte[] serializeMessage(INetMessage msg) {
            MemoryStream buffer = new();
            using (AC2Writer data = new(buffer)) {
                data.Write((uint)msg.opcode);
                msg.write(data);
            }
            return buffer.ToArray();
        }

        private void send(ClientId clientId, INetMessage msg, byte[] payload) {
            clientManager.processClient(clientId, client => {
                client.enqueueBlob(msg.blobFlags, msg.queueId, payload);
                Logs.NET.debug("Enqueued msg",
                    "client", client,
                    "msg", msg,
                    "payload", BitConverter.ToString(payload));
            });
        }

        public void send(ClientId clientId, INetMessage msg) {
            send(clientId, msg, serializeMessage(msg));
        }

        public void send(IEnumerable<ClientId> clientIds, INetMessage msg) {
            byte[] payload = serializeMessage(msg);
            foreach (ClientId clientId in clientIds) {
                send(clientId, msg, payload);
            }
        }
    }
}
