using AC2E.Def;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace AC2E.Server {

    internal class ClientManager {

        private static readonly int MAX_CONNECTIONS = 300;

        public IEnumerable<ClientConnection> clients => _clients.Values;

        private ushort clientIdCounter = 1;
        private readonly Dictionary<ushort, ClientConnection> _clients = new Dictionary<ushort, ClientConnection>();

        public bool tryGetClient(ushort recipientId, out ClientConnection client) {
            lock (this) {
                return _clients.TryGetValue(recipientId, out client);
            }
        }

        public async Task<ClientConnection> addClientAsync(NetInterface netInterface, float serverTime, IPEndPoint clientEndpoint, string accountName) {
            ClientConnection client;
            lock (this) {
                if (_clients.Count > MAX_CONNECTIONS) {
                    return null;
                }
                client = new ClientConnection(clientIdCounter, clientEndpoint, accountName);
                _clients[clientIdCounter] = client;
                clientIdCounter++;
            }

            await client.sendPacketAsync(netInterface, serverTime, new NetPacket {
                connectHeader = new ConnectHeader {
                    connectionAckCookie = client.connectionAckCookie,
                    netId = client.id,
                    outgoingSeed = client.outgoingSeed,
                    incomingSeed = client.incomingSeed,
                },
            });

            return client;
        }

        public void removeClient(ushort clientId) {
            lock (this) {
                _clients.Remove(clientId);
            }
        }
    }
}
