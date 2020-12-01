using AC2RE.Definitions;
using AC2RE.Server.Database;
using System.Collections.Generic;

namespace AC2RE.Server {

    internal class WorldObjectManager {

        private static readonly string INSTANCE_ID_GENERATOR_TYPE = "worldObject";

        private readonly WorldDatabase worldDb;
        private readonly PlayerManager playerManager;
        private readonly InstanceIdGenerator instanceIdGenerator;
        private bool loadedWorld;
        private readonly HashSet<InstanceId> loadedContainers = new();
        private readonly Dictionary<InstanceId, WorldObject> worldObjects = new();

        public WorldObjectManager(WorldDatabase worldDb, PlayerManager playerManager) {
            this.worldDb = worldDb;
            this.playerManager = playerManager;
            instanceIdGenerator = worldDb.getInstanceIdGenerator(INSTANCE_ID_GENERATOR_TYPE) ?? new(INSTANCE_ID_GENERATOR_TYPE);
        }

        public WorldObject? get(InstanceId id) {
            if (!worldObjects.TryGetValue(id, out WorldObject? worldObject)) {
                worldObject = worldDb.getWorldObjectWithId(id);
                if (worldObject != null) {
                    worldObject.init(this, playerManager);
                    worldObjects[id] = worldObject;
                }
            }
            return (worldObject == null || worldObject.deleted) ? null : worldObject;
        }

        private void loadAll() {
            if (!loadedWorld) {
                loadedWorld = true;
                List<WorldObject> dbWorldObjects = worldDb.getAllWorldObjects();
                foreach (WorldObject worldObject in dbWorldObjects) {
                    if (worldObjects.TryAdd(worldObject.id, worldObject)) {
                        worldObject.init(this, playerManager);
                    }
                }
            }
        }

        public List<WorldObject> getAll() {
            loadAll();
            List<WorldObject> resultWorldObjects = new();
            foreach (WorldObject worldObject in worldObjects.Values) {
                if (!worldObject.deleted) {
                    resultWorldObjects.Add(worldObject);
                }
            }
            return resultWorldObjects;
        }

        private void loadWithContainer(InstanceId containerId) {
            if (loadedContainers.Add(containerId)) {
                List<WorldObject> dbWorldObjects = worldDb.getWorldObjectsWithContainer(containerId);
                foreach (WorldObject worldObject in dbWorldObjects) {
                    if (worldObjects.TryAdd(worldObject.id, worldObject)) {
                        worldObject.init(this, playerManager);
                    }
                }
            }
        }

        public List<WorldObject> getWithContainer(InstanceId containerId) {
            loadWithContainer(containerId);
            List<WorldObject> resultWorldObjects = new();
            foreach (WorldObject worldObject in worldObjects.Values) {
                if (!worldObject.deleted && worldObject.containerId == containerId) {
                    resultWorldObjects.Add(worldObject);
                }
            }
            return resultWorldObjects;
        }

        public WorldObject create() {
            WorldObject newObject = new(instanceIdGenerator.next(), this, playerManager);
            worldObjects[newObject.id] = newObject;
            return newObject;
        }

        public void syncNewPlayer(Player player) {
            foreach (WorldObject worldObject in worldObjects.Values) {
                if (worldObject.inWorld) {
                    worldObject.addVisible(player);
                }
            }
        }

        public void broadcastUpdates(double time) {
            foreach (WorldObject worldObject in worldObjects.Values) {
                worldObject.broadcastPhysics(time);
                worldObject.broadcastQualities();
                worldObject.broadcastVisualUpdate();
            }
        }

        public void contributeToSave(WorldSave worldSave) {
            worldSave.idGenerator = instanceIdGenerator;
            worldSave.worldObjects.AddRange(worldObjects.Values);
        }
    }
}
