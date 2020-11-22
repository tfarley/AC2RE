using AC2RE.Definitions;
using AC2RE.Server.Database;
using System.Collections.Generic;

namespace AC2RE.Server {

    internal class WorldObjectManager {

        private static readonly string INSTANCE_ID_GENERATOR_TYPE = "worldObject";

        private readonly WorldDatabase worldDb;
        private readonly PacketHandler packetHandler;
        private readonly PlayerManager playerManager;
        private readonly InstanceIdGenerator instanceIdGenerator;
        private bool loadedWorld;
        private readonly HashSet<InstanceId> loadedContainers = new();
        private readonly Dictionary<InstanceId, WorldObject> worldObjects = new();

        public WorldObjectManager(WorldDatabase worldDb, PacketHandler packetHandler, PlayerManager playerManager) {
            this.worldDb = worldDb;
            this.packetHandler = packetHandler;
            this.playerManager = playerManager;
            instanceIdGenerator = worldDb.getInstanceIdGenerator(INSTANCE_ID_GENERATOR_TYPE) ?? new(INSTANCE_ID_GENERATOR_TYPE);
        }

        public WorldObject? get(InstanceId id) {
            if (!worldObjects.TryGetValue(id, out WorldObject? worldObject)) {
                worldObject = worldDb.getWorldObjectWithId(id);
                if (worldObject != null) {
                    worldObjects[id] = worldObject;
                }
            }
            return (worldObject == null || worldObject.deleted) ? null : worldObject;
        }

        public void enterWorld(WorldObject worldObject) {
            if (!worldObject.inWorld) {
                worldObject.inWorld = true;

                playerManager.broadcastSend(new CreateObjectMsg {
                    id = worldObject.id,
                    visualDesc = worldObject.visual,
                    physicsDesc = worldObject.physics,
                    weenieDesc = worldObject.qualities.weenieDesc,
                });
            }
        }

        public void enterWorld(IEnumerable<WorldObject> worldObjects) {
            foreach (WorldObject worldObject in worldObjects) {
                enterWorld(worldObject);
            }
        }

        public void leaveWorld(WorldObject worldObject) {
            if (worldObject.inWorld) {
                worldObject.inWorld = false;

                playerManager.broadcastSend(new DestroyObjectMsg {
                    idWithStamp = worldObject.getInstanceIdWithStamp(),
                });

                worldObject.instanceStamp++;
            }
        }

        public void leaveWorld(IEnumerable<WorldObject> worldObjects) {
            foreach (WorldObject worldObject in worldObjects) {
                leaveWorld(worldObject);
            }
        }

        private void loadAllInWorld() {
            if (!loadedWorld) {
                loadedWorld = true;
                List<WorldObject> dbWorldObjects = worldDb.getWorldObjectsInWorld();
                foreach (WorldObject worldObject in dbWorldObjects) {
                    worldObjects.TryAdd(worldObject.id, worldObject);
                }
            }
        }

        public List<WorldObject> getAllInWorld() {
            loadAllInWorld();
            List<WorldObject> worldObjects = new();
            foreach (WorldObject worldObject in worldObjects) {
                if (worldObject.inWorld) {
                    worldObjects.Add(worldObject);
                }
            }
            return worldObjects;
        }

        private void loadWithContainer(InstanceId containerId) {
            if (loadedContainers.Add(containerId)) {
                List<WorldObject> dbWorldObjects = worldDb.getWorldObjectsWithContainer(containerId);
                foreach (WorldObject worldObject in dbWorldObjects) {
                    worldObjects.TryAdd(worldObject.id, worldObject);
                }
            }
        }

        public List<WorldObject> getAllInContainer(InstanceId containerId) {
            loadWithContainer(containerId);
            List<WorldObject> contents = new();
            foreach (WorldObject worldObject in worldObjects.Values) {
                if (worldObject.qualities.weenieDesc.containerId == containerId) {
                    contents.Add(worldObject);
                }
            }
            return contents;
        }

        public WorldObject create() {
            WorldObject newObject = new(instanceIdGenerator.next());
            worldObjects[newObject.id] = newObject;
            return newObject;
        }

        public void syncNewClient(ClientId clientId) {
            foreach (WorldObject worldObject in worldObjects.Values) {
                if (worldObject.inWorld) {
                    packetHandler.send(clientId, new CreateObjectMsg {
                        id = worldObject.id,
                        visualDesc = worldObject.visual,
                        physicsDesc = worldObject.physics,
                        weenieDesc = worldObject.qualities.weenieDesc,
                    });
                }
            }
        }

        public void contributeToSave(WorldSave worldSave) {
            worldSave.idGenerator = instanceIdGenerator;
            worldSave.worldObjects.AddRange(worldObjects.Values);
        }
    }
}
