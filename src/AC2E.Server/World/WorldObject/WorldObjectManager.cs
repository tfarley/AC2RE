using AC2E.Def;
using AC2E.Server.Database;
using System.Collections.Generic;

namespace AC2E.Server {

    internal class WorldObjectManager {

        private static readonly string INSTANCE_ID_GENERATOR_TYPE = "worldObject";

        private readonly WorldDatabase worldDb;
        private readonly PacketHandler packetHandler;
        private readonly PlayerManager playerManager;
        private readonly InstanceIdGenerator instanceIdGenerator;
        private readonly Dictionary<InstanceId, WorldObject> worldObjects = new Dictionary<InstanceId, WorldObject>();
        private readonly Dictionary<InstanceId, WorldObject> inWorldObjects = new Dictionary<InstanceId, WorldObject>();

        public WorldObjectManager(WorldDatabase worldDb, PacketHandler packetHandler, PlayerManager playerManager) {
            this.worldDb = worldDb;
            this.packetHandler = packetHandler;
            this.playerManager = playerManager;
            instanceIdGenerator = worldDb.getInstanceIdGenerator(INSTANCE_ID_GENERATOR_TYPE) ?? new InstanceIdGenerator(INSTANCE_ID_GENERATOR_TYPE);
        }

        public WorldObject get(InstanceId id) {
            if (!worldObjects.TryGetValue(id, out WorldObject worldObject)) {
                worldObject = worldDb.getWorldObjectWithId(id);
                worldObjects[id] = worldObject;
            }
            return (worldObject == null || worldObject.deleted) ? null : worldObject;
        }

        public WorldObject getInWorld(InstanceId id) {
            return inWorldObjects.GetValueOrDefault(id, null);
        }

        public void enterWorld(WorldObject worldObject) {
            if (!inWorldObjects.ContainsKey(worldObject.id)) {
                worldObject.inWorld = true;

                inWorldObjects[worldObject.id] = worldObject;

                playerManager.broadcastSend(new CreateObjectMsg {
                    id = worldObject.id,
                    visualDesc = worldObject.visual,
                    physicsDesc = worldObject.physics,
                    weenieDesc = worldObject.weenie,
                });
            }
        }

        public void leaveWorld(WorldObject worldObject) {
            if (inWorldObjects.ContainsKey(worldObject.id)) {
                worldObject.inWorld = false;

                inWorldObjects.Remove(worldObject.id);

                playerManager.broadcastSend(new DestroyObjectMsg {
                    idWithStamp = new InstanceIdWithStamp { id = worldObject.id, instanceStamp = worldObject.instanceStamp, otherStamp = 0 },
                });

                worldObject.instanceStamp++;
            }
        }

        public void loadAllInWorld() {
            foreach (WorldObject worldObject in worldDb.getWorldObjectsInWorld()) {
                if (!worldObjects.ContainsKey(worldObject.id)) {
                    worldObjects[worldObject.id] = worldObject;
                    enterWorld(worldObject);
                }
            }
        }

        public WorldObject create() {
            WorldObject newObject = new WorldObject(instanceIdGenerator.next());
            worldObjects[newObject.id] = newObject;
            return newObject;
        }

        public void syncNewClient(ClientId clientId) {
            foreach (WorldObject worldObject in inWorldObjects.Values) {
                packetHandler.send(clientId, new CreateObjectMsg {
                    id = worldObject.id,
                    visualDesc = worldObject.visual,
                    physicsDesc = worldObject.physics,
                    weenieDesc = worldObject.weenie,
                });
            }
        }

        public void contributeToSave(WorldSave worldSave) {
            worldSave.idGenerator = instanceIdGenerator;
            worldSave.worldObjects.AddRange(worldObjects.Values);
        }
    }
}
