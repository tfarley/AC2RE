using AC2E.Def;
using System.Collections.Generic;

namespace AC2E.Server {

    internal class PlayerManager {

        private readonly PacketHandler packetHandler;
        private readonly Dictionary<ClientId, Player> players = new Dictionary<ClientId, Player>();

        public PlayerManager(PacketHandler packetHandler) {
            this.packetHandler = packetHandler;
        }

        public Player? get(ClientId clientId) {
            return players.GetValueOrDefault(clientId, null);
        }

        public bool exists(ClientId clientId) {
            return players.ContainsKey(clientId);
        }

        public void add(ClientId clientId, Account account) {
            players[clientId] = new Player(clientId, account);
        }

        public void broadcastSend(INetMessage message) {
            packetHandler.send(players.Keys, message);
        }

        public void broadcastSend(ClientId excludeClientId, INetMessage message) {
            packetHandler.send(players.Keys, excludeClientId, message);
        }

        public void disconnectAll() {
            foreach (Player player in players.Values) {
                packetHandler.send(player.clientId, new DisplayStringInfoMsg {
                    type = TextType.ADMIN,
                    text = new StringInfo(new DataId(0x25000626), 165844726),
                });
                disconnect(player);
            }
        }

        public void disconnect(Player player) {
            packetHandler.send(player.clientId, new DoFxMsg {
                senderIdWithStamp = new InstanceIdWithStamp {
                    id = player.characterId,
                    instanceStamp = 5,
                    otherStamp = 9,
                },
                fxId = FxId.ENTER_WORLD,
                scalar = 1.0f,
            });
            packetHandler.send(player.clientId, new InterpCEventPrivateMsg {
                netEvent = new EnterPortalSpaceCEvt()
            });
        }
    }
}
