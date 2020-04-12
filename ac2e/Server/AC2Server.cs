using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using static ProtocolHeader;

class AC2Server {

    private static readonly IPEndPoint ANY_ENDPOINT_V6 = new IPEndPoint(IPAddress.IPv6Any, 0);
    private static readonly IPEndPoint ANY_ENDPOINT = new IPEndPoint(IPAddress.Any, 0);
    private static readonly int MAX_CONNECTIONS = 300;
    private static readonly int PACKET_BUFFER_SIZE = 1200;

    private NetInterface netInterface1;
    private NetInterface netInterface2;
    public bool active;

    private readonly byte[] receiveBuffer = new byte[PACKET_BUFFER_SIZE];
    private readonly byte[] sendBuffer = new byte[PACKET_BUFFER_SIZE];

    private ushort clientCounter = 1;
    private readonly Dictionary<ushort, ClientConnection> clients = new Dictionary<ushort, ClientConnection>();

    private float serverTime => (DateTime.Now.Ticks - Process.GetCurrentProcess().StartTime.Ticks) / TimeSpan.TicksPerSecond;

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
        BinaryReader data = new BinaryReader(new MemoryStream(rawData));

        try {
            Packet packet = new Packet(data);
            ProtocolHeader header = packet.header;

            ALog.debug($"RCVD: s {header.seqId} f {header.flags} c {header.checksum:X8} r {header.recipientId} g {header.interval} l {header.dataLength} i {header.iteration}");

            if (packet.logonHeader != null) {
                ALog.debug($"Logon request: ts {packet.logonHeader.timestamp} acct {packet.logonHeader.account}");
                ClientConnection client = addClient(receiveEndpoint);
                if (client != null) {
                    sendConnect(client);
                } else {
                    ALog.warn("Client tried to connect, but the number of active connections is already at the limit.");
                }
            } else if (clients.TryGetValue(packet.header.recipientId, out ClientConnection client)) {
                if (packet.connectFinalizeHeader != 0) {
                    if (packet.connectFinalizeHeader == client.connectionAckCookie) {
                        ALog.debug($"Got good connect finalize cookie from client id: {packet.header.recipientId}.");
                    } else {
                        ALog.warn($"Got bad connect finalize cookie from client id: {packet.header.recipientId} - {packet.connectFinalizeHeader} sent, {client.connectionAckCookie} expected.");
                    }
                }

                if (packet.header.flags.HasFlag(Flag.ECHO_REQUEST)) {
                    client.echoRequestedLocalTime = packet.echoRequestHeader.localTime;
                    sendEcho(client);
                }
            } else {
                 ALog.warn($"Got packet from unknown client id: {packet.header.recipientId}.");
            }
        } catch (Exception e) {
            ALog.exception(e);
        }
    }

    private ClientConnection addClient(IPEndPoint clientEndpoint) {
        if (clients.Count > MAX_CONNECTIONS) {
            return null;
        }

        ClientConnection client = new ClientConnection(clientCounter, clientEndpoint);
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

    private void sendEcho(ClientConnection client) {
        sendPacket(client, new Packet());
    }

    private void sendPacket(ClientConnection client, Packet packet) {
        float curServerTime = serverTime;

        ProtocolHeader header = packet.header;

        header.seqId = client.seqId;
        client.seqId++;
        header.recipientId = client.id;
        header.interval = (ushort)curServerTime;
        // TODO: Need to advance this?
        header.iteration = 1;

        if (client.echoRequestedLocalTime != -1.0f) {
            packet.echoResponseHeader = new EchoResponseHeader {
                localTime = client.echoRequestedLocalTime,
                localToServerTimeDelta = curServerTime - client.echoRequestedLocalTime,
            };
            client.echoRequestedLocalTime = -1.0f;
        }

        int packetLength = packet.write(new PacketWriter(sendBuffer), client.serverIsaac);
        netInterface1.sendTo(sendBuffer, packetLength, client.endpoint);

        ALog.debug($"Send[{packetLength}] to {client.endpoint} - {BitConverter.ToString(sendBuffer, 0, packetLength)}.", CatTransport.i);

        ALog.debug($"SENT: s {header.seqId} f {header.flags} c {header.checksum:X8} r {header.recipientId} g {header.interval} l {header.dataLength} i {header.iteration}");
    }
}
