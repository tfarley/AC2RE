using AC2E.Def;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace AC2E.Server {

    internal class ClientManager {

        private static readonly int MAX_CONNECTIONS = 300;

        private ushort clientCounter = 1;
        private readonly Dictionary<ushort, ClientConnection> clients = new Dictionary<ushort, ClientConnection>();

        public bool tryGetClient(ushort recipientId, out ClientConnection client) {
            lock (this) {
                return clients.TryGetValue(recipientId, out client);
            }
        }

        public async Task<ClientConnection> addClient(NetInterface netInterface, float serverTime, IPEndPoint clientEndpoint, string accountName) {
            if (clients.Count > MAX_CONNECTIONS) {
                return null;
            }

            ClientConnection client = new ClientConnection(clientCounter, clientEndpoint, accountName);
            lock (this) {
                clients[clientCounter] = client;
                clientCounter++;
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
                clients.Remove(clientId);
            }
        }

        public async Task flushSendAsync(NetInterface netInterface, float serverTime) {
            List<Task> sendTasks = new List<Task>();
            lock (this) {
                foreach (ClientConnection client in clients.Values) {
                    sendTasks.Add(client.flushSendAsync(netInterface, serverTime));
                }
            }
            await Task.WhenAll(sendTasks);
        }
    }
}
