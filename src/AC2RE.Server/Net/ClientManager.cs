using AC2RE.Definitions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace AC2RE.Server;

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

    private byte[] serializeMessage(INetMessage msg) {
        MemoryStream buffer = new();
        using (AC2Writer data = new(buffer)) {
            data.Write((uint)msg.opcode);
            msg.write(data);
        }
        return buffer.ToArray();
    }

    private void send(ClientId clientId, INetMessage msg, byte[] payload, bool ordered = false) {
        tryProcessClient(clientId, client => {
            client.enqueueBlob(msg.blobFlags, msg.queueId, payload, ordered ? msg.orderingType : OrderingType.UNORDERED);

            StringBuilder msgString = new(msg.GetType().Name);
            if (msg is IInterpCEventMsg cEventMsg) {
                msgString.Append($" {cEventMsg.netEvent.funcId}");
            }

            Logs.NET.debug("Enqueued msg",
                "client", client,
                "msg", msgString);
        });
    }

    public void send(ClientId clientId, INetMessage msg, bool ordered = false) {
        send(clientId, msg, serializeMessage(msg), ordered);
    }

    public void send(IEnumerable<ClientId> clientIds, INetMessage msg, bool ordered = false) {
        byte[] payload = serializeMessage(msg);
        foreach (ClientId clientId in clientIds) {
            send(clientId, msg, payload, ordered);
        }
    }
}
