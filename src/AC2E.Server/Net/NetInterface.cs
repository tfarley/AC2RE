using Serilog;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace AC2E.Server {

    internal class NetInterface {

        private static readonly IPEndPoint ANY_ENDPOINT = new IPEndPoint(IPAddress.Any, 0);

        public readonly int port;

        private bool closed;
        private readonly Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        public NetInterface(int port = 0) {
            socket.Bind(new IPEndPoint(IPAddress.Any, port));
            this.port = ((IPEndPoint)socket.LocalEndPoint).Port;
        }

        ~NetInterface() {
            if (!closed) {
                Log.Warning($"Didn't close NetInterface with port {port} before destruction!");
                close();
            }
        }

        internal bool sendTo(byte[] buffer, int size, EndPoint sendEndpoint) {
            // UDP is all-or-nothing when sending a message
            return socket.SendTo(buffer, size, SocketFlags.None, sendEndpoint) == size;
        }

        internal async Task<SocketReceiveFromResult> receiveFromAsync(byte[] buffer) {
            return await socket.ReceiveFromAsync(buffer, SocketFlags.None, ANY_ENDPOINT);
        }

        internal void close() {
            socket.Close();
            closed = true;
        }

        public override string ToString() {
            return $"{port}";
        }
    }
}
