﻿using AC2RE.Definitions;
using System.Collections.Generic;

namespace AC2RE.Server {

    internal class PlayerManager {

        private readonly PacketHandler packetHandler;
        private readonly Dictionary<ClientId, Player> _players = new();
        public IEnumerable<Player> players => _players.Values;

        public PlayerManager(PacketHandler packetHandler) {
            this.packetHandler = packetHandler;
        }

        public Player? get(InstanceId characterId) {
            foreach (Player player in _players.Values) {
                if (player.characterId == characterId) {
                    return player;
                }
            }
            return null;
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

        public void send(Player toPlayer, INetMessage message) {
            packetHandler.send(toPlayer.clientId, message);
        }

        public void send(IEnumerable<Player> toPlayers, INetMessage message) {
            List<ClientId> clientIds = new();
            foreach (Player player in toPlayers) {
                clientIds.Add(player.clientId);
            }
            packetHandler.send(clientIds, message);
        }

        public void sendVisible(InstanceId objectId, IEnumerable<Player> toPlayers, INetMessage message) {
            List<ClientId> clientIds = new();
            foreach (Player player in toPlayers) {
                if (player.visibleObjectIds.Contains(objectId)) {
                    clientIds.Add(player.clientId);
                }
            }
            packetHandler.send(clientIds, message);
        }

        public void sendAll(INetMessage message) {
            packetHandler.send(_players.Keys, message);
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
            foreach (Player player in _players.Values) {
                send(player, new DisplayStringInfoMsg {
                    type = TextType.ADMIN,
                    text = new(new(0x25000626), 165844726),
                });
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
                fxId = FxId.ENTER_WORLD,
                scalar = 1.0f,
            });
            send(player, new InterpCEventPrivateMsg {
                netEvent = new EnterPortalSpaceCEvt()
            });
        }
    }
}
