﻿using AC2RE.Definitions;
using System.Diagnostics.CodeAnalysis;

namespace AC2RE.Server {

    internal abstract class BaseMessageProcessor : IMessageProcessor {

        protected readonly World world;

        public BaseMessageProcessor(World world) {
            this.world = world;
        }

        public abstract bool processMessage(ClientConnection client, Player player, INetMessage genericMsg);

        public bool tryGetInWorld(InstanceId id, [MaybeNullWhen(false)] out WorldObject worldObject) {
            return world.objectManager.tryGetInWorld(id, out worldObject);
        }

        public bool tryGetCharacter(Player player, [MaybeNullWhen(false)] out WorldObject character) {
            return tryGetInWorld(player.characterId, out character);
        }

        protected void send(Player player, INetMessage msg) {
            world.playerManager.send(player, msg);
        }

        protected void sendMessage(Player player, string text, TextType type = TextType.Standard) {
            sendMessage(player, new StringInfo(text), type);
        }

        protected void sendMessage(Player player, StringInfo text, TextType type = TextType.Standard) {
            world.playerManager.send(player, new DisplayStringInfoMsg {
                text = text,
                type = type,
            });
        }

        protected void teleport(Player player, PositionOffset target) {
            if (tryGetCharacter(player, out WorldObject? character)) {
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
