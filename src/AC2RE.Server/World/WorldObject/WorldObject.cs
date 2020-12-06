using AC2RE.Definitions;
using AC2RE.Server.Database;

namespace AC2RE.Server {

    internal partial class WorldObject {

        private World world;

        [DbId]
        public readonly InstanceId id;

        [property: DbPersist]
        public bool destroyed { get; private set; }

        [property: DbPersist]
        public ushort instanceStamp { get; private set; }

        public bool inWorld { get; private set; }

        [DbConstructor]
        private WorldObject(InstanceId id) {
            this.id = id;
        }

        public WorldObject(InstanceId id, World world) : this(id) {
            init(world);

            initContain();
            initEquip();
            initPhysics();
            initQualities();
            initVisual();
        }

        public void init(World world) {
            this.world = world;
        }

        public void destroy() {
            leaveWorld();
            destroyed = true;
        }

        public InstanceIdWithStamp getInstanceIdWithStamp(ushort otherStamp = 0) {
            return new() {
                id = id,
                instanceStamp = instanceStamp,
                otherStamp = otherStamp,
            };
        }

        public void enterWorld() {
            if (inWorld) {
                return;
            }

            inWorld = true;

            // Clear out all dirty values
            broadcastPhysics(0.0);
            broadcastQualities();
            broadcastVisualUpdate();

            world.landblockManager.enterWorld(this);

            lastSentCell = pos.cell;
        }

        public void leaveWorld() {
            if (!inWorld) {
                return;
            }

            inWorld = false;

            world.landblockManager.leaveWorld(this);

            instanceStamp++;
        }
    }
}
