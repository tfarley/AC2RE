using AC2RE.Definitions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace AC2RE.Server;

internal class PacketReceiveManager {

    private readonly List<Language> SUPPORTED_LANGUAGES = new() {
        Language.English,
    };

    private readonly AccountManager accountManager;
    private readonly ClientManager clientManager;
    private readonly ServerTime serverTime;
    private readonly World world;

    public PacketReceiveManager(AccountManager accountManager, ClientManager clientManager, ServerTime serverTime, World world) {
        this.accountManager = accountManager;
        this.clientManager = clientManager;
        this.serverTime = serverTime;
        this.world = world;
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
            if (packet.icmdCommandHeader != null) {
                // TODO: If cmd is EchoRequest, send EchoReply?
                return true;
            }
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
            world.addPlayer(client, account);

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

        if (packet.disconnectErrorHeader != null) {
            Logs.NET.info($"Client disconnected",
                "client", client,
                "error", world.contentManager.translateNetError(packet.disconnectErrorHeader));
            world.removePlayer(client);
            clientManager.removeClient(new(packet.recipientId));
            return;
        }

        if (packet.flags.HasFlag(NetPacket.Flag.LOGOFF)) {
            Logs.NET.info($"Client logged off",
                "client", client);
            world.removePlayer(client);
            clientManager.removeClient(new(packet.recipientId));
            return;
        }

        if (packet.connectionErrorHeader != null) {
            Logs.NET.warn($"Client connection error",
                "client", client,
                "error", world.contentManager.translateNetError(packet.connectionErrorHeader));
        }

        // TODO: Need to handle client acking the re-sent (nacked) packets
        if (packet.flags.HasFlag(NetPacket.Flag.PAK)) {
            client.ackPacket(packet.ackHeader);
        }

        if (packet.nacksHeader != null) {
            foreach (uint seq in packet.nacksHeader) {
                client.nackPacket(seq);
            }
        }

        if (client.connected && packet.seq <= client.highestReceivedPacketSeq) {
            if (!packet.flags.HasFlag(NetPacket.Flag.PAK) && packet.nacksHeader == null) {
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

        if (packet.flags.HasFlag(NetPacket.Flag.CONNECT_ACK)) {
            if (packet.connectAckHeader == client.connectionAckCookie) {
                Logs.NET.info("Client connected",
                    "client", client);
                client.connect();
                send(client.id, new WorldNameMsg {
                    worldName = new("MyWorld"),
                });
                send(client.id, new CliDatInterrogationMsg {
                    regionId = (RegionID)1,
                    nameRuleLanguage = Language.English,
                    supportedLanguages = SUPPORTED_LANGUAGES,
                });
            } else {
                Logs.NET.debug("Got bad connect ack cookie",
                    "client", client,
                    "sentCookie", packet.connectAckHeader,
                    "expectedCookie", client.connectionAckCookie);
            }
        }

        if (packet.echoRequestHeader != null) {
            client.echoRequestedLocalTime = packet.echoRequestHeader.localTime;
            client.echoRequestedServerTime = serverTime.elapsedTime;
        }

        if (packet.frags.Count > 0) {
            foreach (NetBlobFrag frag in packet.frags) {
                client.incomingBlobQueue.addFragment(frag);
            }
        }
    }

    public void processNetBlobs() {
        clientManager.processClients(client => {
            if (!client.connected) {
                return;
            }

            while (client.incomingBlobQueue.TryDequeue(out NetBlob? blob)) {
                try {
                    using (AC2Reader data = new(new MemoryStream(blob.payload))) {

                        MessageOpcode opcode = data.ReadEnum<MessageOpcode>();
                        INetMessage genericMsg = INetMessage.read(opcode, data, true);

                        StringBuilder msgString = new(genericMsg.GetType().Name);
                        if (opcode == MessageOpcode.Interp__InterpSEvent) {
                            InterpSEventMsg msg = (InterpSEventMsg)genericMsg;
                            msgString.Append($" {msg.netEvent.funcId}");
                        }

                        Logs.NET.debug("Got msg",
                            "client", client,
                            "msg", msgString);

                        if (!world.processMessage(client, genericMsg)) {
                            Logs.NET.warn("Unhandled message",
                                "client", client,
                                "msg", msgString);
                        }

                        if (data.BaseStream.Position < data.BaseStream.Length) {
                            Logs.NET.warn("NetBlob was not fully read",
                                "msg", msgString,
                                "client", client,
                                "pos", data.BaseStream.Position,
                                "len", data.BaseStream.Length);
                        }
                    }
                } catch (Exception e) {
                    Logs.NET.error(e, "Error processing NetBlob");
                }
            }
        });
    }

    private byte[] serializeMessage(INetMessage msg) {
        MemoryStream buffer = new();
        using (AC2Writer data = new(buffer)) {
            data.WriteEnum(msg.opcode);
            msg.write(data);
        }
        return buffer.ToArray();
    }

    private void send(ClientId clientId, INetMessage msg, byte[] payload, bool ordered = false) {
        clientManager.tryProcessClient(clientId, client => {
            client.enqueueBlob(msg.blobFlags, msg.queueId, payload, ordered ? msg.orderingType : OrderingType.UNORDERED);

            StringBuilder msgString = new(msg.GetType().Name);
            if (msg is IInterpCEventMsg cEventMsg) {
                msgString.Append($" {cEventMsg.netEvent.funcId}");
            }

            Logs.NET.debug("Enqueued msg",
                "client", client,
                "msg", msgString);
        });
    }

    public void send(ClientId clientId, INetMessage msg, bool ordered = false) {
        send(clientId, msg, serializeMessage(msg), ordered);
    }

    public void send(IEnumerable<ClientId> clientIds, INetMessage msg, bool ordered = false) {
        byte[] payload = serializeMessage(msg);
        foreach (ClientId clientId in clientIds) {
            send(clientId, msg, payload, ordered);
        }
    }
}
