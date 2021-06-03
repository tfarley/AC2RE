using AC2RE.Definitions;

namespace AC2RE.Server {

    internal partial class WorldObject {

        private World world;

        public readonly InstanceId id;
        public bool deleted { get; private set; }

        public bool persistent;

        public bool inWorld { get; private set; }

        public Player? player;
        public bool attacking { get; private set; }

        public WorldObject(World world, InstanceId id, bool persistent) {
            this.world = world;
            this.id = id;
            this.persistent = persistent;

            initContain();
            initEquip();
            initPhysics();
            initQualities();
            initSkills();
            initVisual();
        }

        public void delete() {
            leaveWorld();
            deleted = true;
        }

        public InstanceIdWithStamp getInstanceIdWithStamp(ushort otherStamp = 0) {
            return new() {
                id = id,
                instanceStamp = physics.instanceStamp,
                otherStamp = otherStamp,
            };
        }

        public void enterWorld() {
            if (inWorld) {
                return;
            }

            inWorld = true;

            // Clear out all dirty values
            broadcastUpdates();

            world.landblockManager.enterWorld(this);

            lastSentCell = pos.cell;
        }

        public void leaveWorld() {
            if (!inWorld) {
                return;
            }

            inWorld = false;

            world.landblockManager.leaveWorld(this);

            physics.instanceStamp++;
        }


        public void broadcastUpdates() {
            broadcastPhysics();
            broadcastQualities();
            broadcastVisuals();
        }

        public void setAttacking(bool attacking) {
            if (this.attacking == attacking) {
                return;
            }

            this.attacking = attacking;
            world.playerManager.send(player, new InterpCEventPrivateMsg {
                netEvent = new UpdateAttackStateCEvt() {
                    attacking = attacking,
                }
            }, true);
        }
    }
}
