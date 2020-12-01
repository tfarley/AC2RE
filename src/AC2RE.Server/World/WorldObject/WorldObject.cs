using AC2RE.Definitions;
using AC2RE.Server.Database;

namespace AC2RE.Server {

    internal partial class WorldObject {

        [DatabaseIgnore]
        public WorldObjectManager objectManager;

        [DatabaseIgnore]
        public PlayerManager playerManager;

        public readonly InstanceId id;
        public bool deleted;

        [DatabaseIgnore]
        public ushort instanceStamp;

        [DatabaseIgnore]
        public bool inWorld;

        // For deserialization
        private WorldObject(InstanceId id) {
            this.id = id;
        }

        public WorldObject(InstanceId id, WorldObjectManager objectManager, PlayerManager playerManager) : this(id) {
            init(objectManager, playerManager);

            initContain();
            initEquip();
            initPhysics();
            initQualities();
            initVisual();
        }

        public void init(WorldObjectManager objectManager, PlayerManager playerManager) {
            this.objectManager = objectManager;
            this.playerManager = playerManager;
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

            // Clear out all dirty values
            broadcastPhysics(0.0);
            broadcastQualities();
            broadcastVisualUpdate();

            CreateObjectMsg msg = new() {
                id = id,
                visualDesc = visual,
                physicsDesc = physics,
                weenieDesc = qualities.weenieDesc,
            };

            foreach (Player player in playerManager.players) {
                player.visibleObjectIds.Add(id);
                playerManager.send(player, msg);
            }

            inWorld = true;
        }

        public void leaveWorld() {
            if (!inWorld) {
                return;
            }

            DestroyObjectMsg msg = new() {
                idWithStamp = getInstanceIdWithStamp(),
            };

            foreach (Player player in playerManager.players) {
                player.visibleObjectIds.Remove(id);
                playerManager.send(player, msg);
            }

            inWorld = false;

            instanceStamp++;
        }

        public void addVisible(Player player) {
            if (!player.visibleObjectIds.Contains(id)) {
                player.visibleObjectIds.Add(id);
                playerManager.send(player, new CreateObjectMsg {
                    id = id,
                    visualDesc = visual,
                    physicsDesc = physics,
                    weenieDesc = qualities.weenieDesc,
                });
            }
        }

        public void removeVisible(Player player) {
            if (player.visibleObjectIds.Contains(id)) {
                player.visibleObjectIds.Remove(id);
                playerManager.send(player, new DestroyObjectMsg {
                    idWithStamp = getInstanceIdWithStamp(),
                });
            }
        }
    }
}
