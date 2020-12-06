using AC2RE.Definitions;
using AC2RE.Server.Database;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AC2RE.Server {

    internal class WorldObjectManager {

        private static readonly string INSTANCE_ID_GENERATOR_TYPE = "worldObject";

        private readonly World world;
        private readonly InstanceIdGenerator instanceIdGenerator;
        private bool loadedWorld;
        private readonly Dictionary<InstanceId, WorldObject> worldObjects = new();

        public WorldObjectManager(World world) {
            this.world = world;
            instanceIdGenerator = world.worldDb.getInstanceIdGenerator(INSTANCE_ID_GENERATOR_TYPE) ?? new(INSTANCE_ID_GENERATOR_TYPE);
        }

        public InstanceIdWithStamp getStamp(InstanceId id, ushort otherStamp = 0) {
            // Includes destroyed objects
            if (worldObjects.TryGetValue(id, out WorldObject? worldObject)) {
                return worldObject.getInstanceIdWithStamp(otherStamp);
            }

            return new InstanceIdWithStamp {
                id = id,
                instanceStamp = 0,
                otherStamp = otherStamp,
            };
        }

        public bool tryGet(InstanceId id, [MaybeNullWhen(false)] out WorldObject worldObject) {
            worldObject = get(id);
            return worldObject != null;
        }

        public WorldObject? get(InstanceId id) {
            if (!worldObjects.TryGetValue(id, out WorldObject? worldObject)) {
                worldObject = world.worldDb.getWorldObjectWithId(id);
                if (worldObject != null) {
                    worldObject.init(world);
                    worldObjects[id] = worldObject;
                }
            }
            return (worldObject != null && !worldObject.destroyed) ? worldObject : null;
        }

        private void loadAll() {
            if (!loadedWorld) {
                loadedWorld = true;
                List<WorldObject> dbWorldObjects = world.worldDb.getAllWorldObjects();
                foreach (WorldObject worldObject in dbWorldObjects) {
                    if (worldObjects.TryAdd(worldObject.id, worldObject)) {
                        worldObject.init(world);
                    }
                }
            }
        }

        public List<WorldObject> getAll() {
            loadAll();
            List<WorldObject> resultWorldObjects = new();
            foreach (WorldObject worldObject in worldObjects.Values) {
                if (!worldObject.destroyed) {
                    resultWorldObjects.Add(worldObject);
                }
            }
            return resultWorldObjects;
        }

        public List<WorldObject> loadWithLandblockId(LandblockId landblockId) {
            List<WorldObject> newWorldObjects = new();
            List<WorldObject> dbWorldObjects = world.worldDb.getWorldObjectsWithLandblockId(landblockId);
            foreach (WorldObject worldObject in dbWorldObjects) {
                if (worldObjects.TryGetValue(worldObject.id, out WorldObject? loadedObject)) {
                    newWorldObjects.Add(loadedObject);
                } else {
                    worldObjects[worldObject.id] = worldObject;
                    worldObject.init(world);
                    newWorldObjects.Add(worldObject);
                }
            }
            return newWorldObjects;
        }

        public WorldObject create() {
            WorldObject newObject = new(instanceIdGenerator.next(), world);
            worldObjects[newObject.id] = newObject;
            return newObject;
        }

        public void broadcastUpdates() {
            foreach (WorldObject worldObject in worldObjects.Values) {
                worldObject.broadcastPhysics(world.serverTime.time);
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
