using AC2RE.Definitions;

namespace AC2RE.Server {

    internal abstract class BaseMessageProcessor : IMessageProcessor {

        protected readonly World world;

        public BaseMessageProcessor(World world) {
            this.world = world;
        }

        public abstract bool processMessage(ClientConnection client, Player player, INetMessage genericMsg);

        protected void send(Player player, INetMessage msg) {
            world.playerManager.send(player, msg);
        }

        protected void sendMessage(Player player, string text, TextType type = TextType.ADMIN) {
            world.playerManager.send(player, new InterpCEventPrivateMsg {
                netEvent = new DisplayStringInfoCEvt {
                    text = new($"{text}\n"),
                    type = type,
                }
            });
        }

        protected void teleport(Player player, PositionOffset target) {
            if (world.objectManager.tryGet(player.characterId, out WorldObject? character) && character.inWorld) {
                PositionPack posPack = new() {
                    time = world.serverTime.time,
                    offset = target,
                    heading = new(character.heading),
                    packFlags = PositionPack.PackFlag.CONTACT,
                    posStamp = ++character.physics.timestamps[(int)PhysicsTimeStamp.POSITION],
                    forcePosStamp = character.physics.timestamps[(int)PhysicsTimeStamp.FORCE_POSITION],
                    teleportStamp = ++character.physics.timestamps[(int)PhysicsTimeStamp.TELEPORT],
                };

                world.playerManager.send(player, new PositionCellMsg {
                    senderIdWithStamp = character.getInstanceIdWithStamp(),
                    posPack = posPack,
                });
            }
        }
    }
}
