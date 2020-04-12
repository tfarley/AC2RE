using System;
using System.Net;
using System.Net.Sockets;

public class NetInterface {

    internal AddressFamily addressFamily => socket.AddressFamily;

    public bool available => socket.Available > 0;

    public int port => ((IPEndPoint)socket.LocalEndPoint).Port;

    // TODO: Also implement out-of-order packets via delayed/queued buffers (easier on rcv side, since you can fake 'available' data to rcv after the delay has elapsed that just pulls from queue)
    public int simPacketLossRcvPct;
    public int simPacketLossSendPct;

    private Socket socket;

    private readonly int randomSeed = new Random().Next();
    private readonly Random random;

    public NetInterface(AddressFamily addressFamily, int port = 0) {
        // Client socket doesn't seem to like sending to an IPv4 address if its own family is IPv6, so this uses the target IP's family
        socket = new Socket(addressFamily, SocketType.Dgram, ProtocolType.Udp);
        socket.Blocking = false;

        if (addressFamily == AddressFamily.InterNetworkV6) {
            socket.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPv6Only, false);
            socket.DualMode = true;
            socket.Bind(new IPEndPoint(IPAddress.IPv6Any, port));
        } else {
            socket.Bind(new IPEndPoint(IPAddress.Any, port));
        }

        random = new Random(randomSeed);

        ALog.debug($"Initialized interface with random seed {randomSeed}.", CatProto.i);
    }

    ~NetInterface() {
        // Always close in order to release system resources
        // (i.e. those that GC is not aware of, such as a socket)
        if (socket != null) {
            ALog.warn($"Didn't close NetInterface with port {port} before destruction!");
            close();
        }
    }

    internal bool sendTo(byte[] sendBuffer, int size, EndPoint sendEndpoint) {
        if (random.Next(0, 100) < simPacketLossSendPct) {
            return true;
        }

        // UDP is all-or-nothing when sending a message
        return socket.SendTo(sendBuffer, size, SocketFlags.None, sendEndpoint) == size;
    }

    internal int receiveFrom(byte[] receiveBuffer, ref EndPoint receiveEndpoint) {
        // NOTE: Must check available first, or else will get exception for non-blocking sockets
        int receivedBytes = socket.ReceiveFrom(receiveBuffer, ref receiveEndpoint);
        return random.Next(0, 100) < simPacketLossRcvPct ? 0 : receivedBytes;
    }

    internal void close() {
        socket.Close();
        socket = null;
    }
}
