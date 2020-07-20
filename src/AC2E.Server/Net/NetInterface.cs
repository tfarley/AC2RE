using Serilog;
using System.Net;
using System.Net.Sockets;

namespace AC2E.Server {

    internal class NetInterface {

        public bool available => socket.Available > 0;

        public int port => ((IPEndPoint)socket.LocalEndPoint).Port;

        private Socket socket;

        public NetInterface(int port = 0) {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Blocking = false;
            socket.Bind(new IPEndPoint(IPAddress.Any, port));
        }

        ~NetInterface() {
            // Always close in order to release system resources
            // (i.e. those that GC is not aware of, such as a socket)
            if (socket != null) {
                Log.Warning($"Didn't close NetInterface with port {port} before destruction!");
                close();
            }
        }

        internal bool sendTo(byte[] sendBuffer, int size, EndPoint sendEndpoint) {
            // UDP is all-or-nothing when sending a message
            return socket.SendTo(sendBuffer, size, SocketFlags.None, sendEndpoint) == size;
        }

        internal int receiveFrom(byte[] receiveBuffer, ref EndPoint receiveEndpoint) {
            // NOTE: Must check available first, or else will get exception for non-blocking sockets
            return socket.ReceiveFrom(receiveBuffer, ref receiveEndpoint);
        }

        internal void close() {
            socket.Close();
            socket = null;
        }
    }
}
