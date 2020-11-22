using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace AC2RE.Server {

    internal class ClientManager {

        private static readonly int MAX_CONNECTIONS = 300;

        public IEnumerable<ClientConnection> clients => _clients.Values;

        private readonly ClientIdGenerator clientIdGenerator = new();
        private readonly Dictionary<ClientId, ClientConnection> _clients = new();

        public bool tryGetClient(ClientId clientId, [MaybeNullWhen(false)] out ClientConnection client) {
            lock (this) {
                return _clients.TryGetValue(clientId, out client);
            }
        }

        public ClientConnection? addClient(NetInterface netInterface, double time, float elapsedTime, IPEndPoint clientEndpoint, Account account) {
            ClientConnection client;
            lock (this) {
                if (_clients.Count > MAX_CONNECTIONS) {
                    return null;
                }
                ClientId clientId = clientIdGenerator.next();
                client = new(clientId, clientEndpoint, account);
                _clients[clientId] = client;
            }

            client.sendPacket(netInterface, time, elapsedTime, new() {
                connectHeader = new() {
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
