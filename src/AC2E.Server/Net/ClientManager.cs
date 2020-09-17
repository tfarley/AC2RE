using AC2E.Def;
using System.Collections.Generic;
using System.Net;

namespace AC2E.Server {

    internal class ClientManager {

        private static readonly int MAX_CONNECTIONS = 300;

        public IEnumerable<ClientConnection> clients => _clients.Values;

        private readonly ClientIdGenerator clientIdGenerator = new ClientIdGenerator();
        private readonly Dictionary<ClientId, ClientConnection> _clients = new Dictionary<ClientId, ClientConnection>();

        public bool tryGetClient(ClientId clientId, out ClientConnection client) {
            lock (this) {
                return _clients.TryGetValue(clientId, out client);
            }
        }

        public ClientConnection addClient(NetInterface netInterface, double time, float elapsedTime, IPEndPoint clientEndpoint, Account account) {
            ClientConnection client;
            lock (this) {
                if (_clients.Count > MAX_CONNECTIONS) {
                    return null;
                }
                ClientId clientId = clientIdGenerator.next();
                client = new ClientConnection(clientId, clientEndpoint, account);
                _clients[clientId] = client;
            }

            client.sendPacket(netInterface, time, elapsedTime, new NetPacket {
                connectHeader = new ConnectHeader {
                    connectionAckCookie = client.connectionAckCookie,
                    netId = client.id.id,
                    outgoingSeed = client.outgoingSeed,
                    incomingSeed = client.incomingSeed,
                },
            });

            return client;
        }

        public void removeClient(ClientId clientId) {
            lock (this) {
                _clients.Remove(clientId);
            }
        }
    }
}
