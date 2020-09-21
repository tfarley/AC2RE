using AC2E.Def;
using AC2E.Server.Database;
using System.Collections.Generic;

namespace AC2E.Server {

    internal class WorldObjectManager {

        private static readonly string INSTANCE_ID_GENERATOR_TYPE = "worldObject";

        private readonly WorldDatabase worldDb;
        private readonly InstanceIdGenerator instanceIdGenerator;
        private readonly Dictionary<InstanceId, WorldObject> worldObjects = new Dictionary<InstanceId, WorldObject>();

        public WorldObjectManager(WorldDatabase worldDb) {
            this.worldDb = worldDb;
            instanceIdGenerator = worldDb.getInstanceIdGenerator(INSTANCE_ID_GENERATOR_TYPE) ?? new InstanceIdGenerator(INSTANCE_ID_GENERATOR_TYPE);
        }

        public WorldObject get(InstanceId id) {
            if (!worldObjects.TryGetValue(id, out WorldObject worldObject)) {
                worldObject = worldDb.getWorldObjectWithId(id);
                worldObjects[id] = worldObject;
            }
            return (worldObject == null || worldObject.deleted) ? null : worldObject;
        }

        public WorldObject create() {
            WorldObject newObject = new WorldObject(instanceIdGenerator.next());
            worldObjects[newObject.id] = newObject;
            return newObject;
        }

        public void contributeToSave(WorldSave worldSave) {
            worldSave.idGenerator = instanceIdGenerator;
            worldSave.worldObjects.AddRange(worldObjects.Values);
        }
    }
}
