using Serilog;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace AC2E.Server {

    internal class NetInterface {

        public static readonly IPEndPoint ANY_ENDPOINT = new IPEndPoint(IPAddress.Any, 0);

        public int port => ((IPEndPoint)socket.LocalEndPoint).Port;

        private Socket socket;

        public NetInterface(int port = 0) {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Bind(new IPEndPoint(IPAddress.Any, port));
        }

        ~NetInterface() {
            if (socket != null) {
                Log.Warning($"Didn't close NetInterface with port {port} before destruction!");
                close();
            }
        }

        internal async Task<bool> sendToAsync(byte[] sendBuffer, int size, EndPoint sendEndpoint) {
            // UDP is all-or-nothing when sending a message
            return await socket.SendToAsync(new ArraySegment<byte>(sendBuffer, 0, size), SocketFlags.None, sendEndpoint) == size;
        }

        internal async Task<SocketReceiveFromResult> receiveFromAsync(byte[] receiveBuffer) {
            return await socket.ReceiveFromAsync(receiveBuffer, SocketFlags.None, ANY_ENDPOINT);
        }

        internal void close() {
            socket.Close();
            socket = null;
        }

        public override string ToString() {
            return $"{port}";
        }
    }
}
