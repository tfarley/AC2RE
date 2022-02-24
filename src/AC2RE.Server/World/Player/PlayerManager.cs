using AC2RE.Definitions;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AC2RE.Server;

internal class PlayerManager {

    private readonly ClientManager clientManager;

    private readonly Dictionary<ClientId, Player> _players = new();
    public IEnumerable<Player> players => _players.Values;

    public PlayerManager(ClientManager clientManager) {
        this.clientManager = clientManager;
    }

    public bool tryGet(InstanceId characterId, [NotNullWhen(true)] out Player? player) {
        player = get(characterId);
        return player != null;
    }

    public Player? get(InstanceId characterId) {
        if (characterId != InstanceId.NULL) {
            foreach (Player player in _players.Values) {
                if (player.characterId == characterId) {
                    return player;
                }
            }
        }
        return null;
    }

    public bool tryGet(ClientId clientId, [NotNullWhen(true)] out Player? player) {
        player = get(clientId);
        return player != null;
    }

    public Player? get(ClientId clientId) {
        return _players.GetValueOrDefault(clientId);
    }

    public bool exists(ClientId clientId) {
        return _players.ContainsKey(clientId);
    }

    public void add(ClientId clientId, Account account) {
        _players[clientId] = new(clientId, account);
    }

    public void remove(ClientId clientId) {
        _players.Remove(clientId);
    }

    public void send(Player toPlayer, IClientEvent netEvent) {
        send(toPlayer, new InterpCEventPrivateMsg {
            netEvent = netEvent,
        });
    }

    public void send(Player toPlayer, INetMessage message) {
        clientManager.send(toPlayer.clientId, message);
    }

    public void send(IEnumerable<Player> toPlayers, INetMessage message) {
        List<ClientId> clientIds = new();
        foreach (Player player in toPlayers) {
            clientIds.Add(player.clientId);
        }
        clientManager.send(clientIds, message);
    }

    public void sendAll(INetMessage message) {
        clientManager.send(_players.Keys, message);
    }

    public void sendAllVisible(InstanceId objectId, INetMessage message) {
        foreach (Player player in _players.Values) {
            if (player.visibleObjectIds.Contains(objectId)) {
                send(player, message);
            }
        }
    }

    public void sendAllExcept(Player? excludePlayer, INetMessage message) {
        foreach (Player player in _players.Values) {
            if (player != excludePlayer) {
                send(player, message);
            }
        }
    }

    public void sendAllVisibleExcept(InstanceId objectId, Player? excludePlayer, INetMessage message) {
        foreach (Player player in _players.Values) {
            if (player != excludePlayer && player.visibleObjectIds.Contains(objectId)) {
                send(player, message);
            }
        }
    }

    public void disconnectAll() {
        sendAll(new DisplayStringInfoMsg {
            type = TextType.Admin,
            text = new(new(0x25000626), new(0x9E296F6)),
        });
        foreach (Player player in _players.Values) {
            disconnect(player);
        }
    }

    public void disconnect(Player player) {
        send(player, new DoFxMsg {
            senderIdWithStamp = new() {
                id = player.characterId,
                instanceStamp = 5,
                otherStamp = 9,
            },
            fxId = FxId.Enter_World,
            scalar = 1.0f,
        });
        send(player, new EnterPortalSpaceCEvt());
    }
}
