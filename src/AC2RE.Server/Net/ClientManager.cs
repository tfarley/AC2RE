using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;

namespace AC2RE.Server {

    internal class ClientManager {

        private static readonly int MAX_CLIENTS = 300;

        private readonly ClientIdGenerator clientIdGenerator = new();
        private readonly ConcurrentDictionary<ClientId, ClientConnection> _clients = new();

        public void processClients(Action<ClientConnection> action) {
            foreach (ClientConnection client in _clients.Values) {
                lock (client) {
                    action.Invoke(client);
                }
            }
        }

        public void processClient(ClientId clientId, Action<ClientConnection> action) {
            ClientConnection client = _clients[clientId];
            lock (client) {
                action.Invoke(client);
            }
        }

        public bool tryProcessClient(ClientId clientId, Action<ClientConnection> action) {
            if (_clients.TryGetValue(clientId, out ClientConnection? client)) {
                lock (client) {
                    action.Invoke(client);
                }
                return true;
            }

            return false;
        }

        public ClientId addClient(uint connectionSeq, IPEndPoint clientEndpoint, Account account) {
            if (_clients.Count > MAX_CLIENTS) {
                return ClientId.NULL;
            }

            ClientConnection client = new(clientIdGenerator.next(), connectionSeq, clientEndpoint, account);
            _clients[client.id] = client;

            return client.id;
        }

        public void removeClient(ClientId clientId) {
            _clients.Remove(clientId, out _);
        }
    }
}
