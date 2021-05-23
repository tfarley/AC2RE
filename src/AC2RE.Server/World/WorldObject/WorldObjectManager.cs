using AC2RE.Definitions;
using AC2RE.Server.Database;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AC2RE.Server {

    internal class WorldObjectManager {

        private static readonly string INSTANCE_ID_GENERATOR_TYPE = "worldObject";

        private readonly World world;
        private readonly ContentManager contentManager;
        private readonly InstanceIdGenerator instanceIdGenerator;
        private bool loadedWorld;
        private readonly Dictionary<InstanceId, WorldObject> worldObjects = new();

        public WorldObjectManager(World world, ContentManager contentManager) {
            this.world = world;
            this.contentManager = contentManager;
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
                worldObject = world.worldDb.getWorldObjectWithId(world, id);
                if (worldObject != null) {
                    world.worldDb.initWorldObject(world, worldObject);
                    worldObjects[id] = worldObject;
                }
            }
            return (worldObject != null && !worldObject.destroyed) ? worldObject : null;
        }

        private void loadAll() {
            if (!loadedWorld) {
                loadedWorld = true;
                List<WorldObject> dbWorldObjects = world.worldDb.getAllWorldObjects(world);
                foreach (WorldObject worldObject in dbWorldObjects) {
                    if (worldObjects.TryAdd(worldObject.id, worldObject)) {
                        world.worldDb.initWorldObject(world, worldObject);
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
            List<WorldObject> dbWorldObjects = world.worldDb.getWorldObjectsWithLandblockId(world, landblockId);
            List<WorldObject> newWorldObjects = filterNewWorldObjects(dbWorldObjects);
            List<MapObject> mapObjects = world.mapDb.getMapObjectsInLandblock(landblockId);
            foreach (MapObject mapObject in mapObjects) {
                WorldObject mapWorldObject = world.objectManager.create(mapObject.id, mapObject.entityDid);
                mapWorldObject.physics.pos = mapObject.position;
                mapWorldObject.qualities.weenieDesc.scale = mapObject.scale;
                mapWorldObject.qualities.weenieDesc.name = mapObject.name;

                mapWorldObject.enterWorld();

                newWorldObjects.Add(mapWorldObject);
            }
            return newWorldObjects;
        }

        public List<WorldObject> loadWithContainerId(InstanceId containerId) {
            return filterNewWorldObjects(world.worldDb.getWorldObjectsWithContainerId(world, containerId));
        }

        public List<WorldObject> loadWithEquipperId(InstanceId equipperId) {
            return filterNewWorldObjects(world.worldDb.getWorldObjectsWithEquipperId(world, equipperId));
        }

        public List<WorldObject> loadWithParentId(InstanceId parentId) {
            return filterNewWorldObjects(world.worldDb.getWorldObjectsWithParentId(world, parentId));
        }

        private List<WorldObject> filterNewWorldObjects(List<WorldObject> dbWorldObjects) {
            List<WorldObject> newWorldObjects = new();
            foreach (WorldObject worldObject in dbWorldObjects) {
                if (worldObjects.TryGetValue(worldObject.id, out WorldObject? loadedObject)) {
                    newWorldObjects.Add(loadedObject);
                } else {
                    worldObjects[worldObject.id] = worldObject;
                    world.worldDb.initWorldObject(world, worldObject);
                    newWorldObjects.Add(worldObject);
                }
            }
            return newWorldObjects;
        }

        public WorldObject create(InstanceId id, DataId entityDid, DataId physicsEntityDid = default) {
            return create(entityDid, physicsEntityDid, false, id);
        }

        public WorldObject create(DataId entityDid = default, DataId physicsEntityDidOverride = default, bool persistent = false, InstanceId id = default) {
            if (id == InstanceId.NULL) {
                id = instanceIdGenerator.next();
            }
            WorldObject newObject = new(world, id, persistent);
            newObject.entityDid = entityDid;
            applyEntities(newObject, physicsEntityDidOverride);
            worldObjects[newObject.id] = newObject;
            return newObject;
        }

        public void applyEntities(WorldObject worldObject, DataId physicsEntityDidOverride = default) {
            if (worldObject.entityDid != DataId.NULL) {
                EntityDef entityDef = contentManager.getEntityDef(worldObject.entityDid);
                if (entityDef.type != EntityType.WEENIE) {
                    throw new ArgumentException(entityDef.type.ToString());
                }

                DataId qualitiesDid = new(0x81000000 + worldObject.entityDid.id - DbTypeDef.TYPE_TO_DEF[DbType.ENTITYDESC].baseDid.id);
                CBaseQualities baseQualities = contentManager.getQualities(qualitiesDid);

                worldObject.qualities.ints = baseQualities.ints != null ? new(baseQualities.ints) : null;
                worldObject.qualities.longs = baseQualities.longs != null ? new(baseQualities.longs) : null;
                worldObject.qualities.bools = baseQualities.bools != null ? new(baseQualities.bools) : null;
                worldObject.qualities.floats = baseQualities.floats != null ? new(baseQualities.floats) : null;
                worldObject.qualities.doubles = baseQualities.doubles != null ? new(baseQualities.doubles) : null;
                worldObject.qualities.strings = baseQualities.strings != null ? new(baseQualities.strings) : null;
                worldObject.qualities.dids = baseQualities.dids != null ? new(baseQualities.dids) : null;
                worldObject.qualities.ids = baseQualities.ids != null ? new(baseQualities.ids) : null;
                worldObject.qualities.poss = baseQualities.poss != null ? new(baseQualities.poss) : null;
                worldObject.qualities.stringInfos = baseQualities.stringInfos != null ? new(baseQualities.stringInfos) : null; // TODO: Need to do a deep copy of the StringInfos?
                worldObject.qualities.packageIds = baseQualities.packageIds != null ? new(baseQualities.packageIds) : null;

                WeenieDesc baseWeenie = baseQualities.weenieDesc;
                worldObject.qualities.did = baseQualities.did;
                worldObject.packageType = entityDef.packageType;
                worldObject.name = baseWeenie.name;
                worldObject.pluralName = baseWeenie.pluralName;
                worldObject.iconDid = baseWeenie.iconDid;
                worldObject.containerId = baseWeenie.containerId;
                worldObject.wielderId = baseWeenie.wielderId;
                worldObject.monarchId = baseWeenie.monarchId;
                worldObject.originatorId = baseWeenie.originatorId;
                worldObject.claimantId = baseWeenie.claimantId;
                worldObject.killerId = baseWeenie.killerId;
                worldObject.summonerId = baseWeenie.petSummonerId;
                worldObject.quantity = baseWeenie.quantity;
                worldObject.value = baseWeenie.value;
                worldObject.faction = baseWeenie.factionType;
                worldObject.pkAlwaysTruePermissions = baseWeenie.pkAlwaysTruePermissions;
                worldObject.pkAlwaysFalsePermissions = baseWeenie.pkAlwaysFalsePermissions;
                worldObject.physicsTypeLow = baseWeenie.physicsTypeLow;
                worldObject.physicsTypeHigh = baseWeenie.physicsTypeHigh;
                worldObject.movementEtherealLow = baseWeenie.movementEtherealLow;
                worldObject.movementEtherealHigh = baseWeenie.movementEtherealHigh;
                worldObject.placementEtherealLow = baseWeenie.placementEtherealLow;
                worldObject.placementEtherealHigh = baseWeenie.placementEtherealHigh;
                worldObject.durability = baseWeenie.durabilityCurrentLevel;
                worldObject.durabilityMax = baseWeenie.durabilityMaxLevel;
                worldObject.scale = baseWeenie.scale;
                worldObject.qualities.weenieDesc.bitfield = baseWeenie.bitfield;

                worldObject.syncWeenieDesc();
            }

            if (physicsEntityDidOverride != default) {
                worldObject.physicsEntityDid = physicsEntityDidOverride;
            }

            if (worldObject.physicsEntityDid != DataId.NULL) {
                EntityDef physicsEntityDef = contentManager.getEntityDef(worldObject.physicsEntityDid);
                if (physicsEntityDef.type != EntityType.PHYSICS) {
                    throw new ArgumentException(physicsEntityDef.type.ToString());
                }

                worldObject.visual.parentDid = physicsEntityDef.dataId;
            }
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
            foreach (WorldObject worldObject in worldObjects.Values) {
                if (worldObject.persistent) {
                    worldSave.worldObjects.Add(worldObject);
                }
            }
        }
    }
}
