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
            return (worldObject != null && !worldObject.deleted) ? worldObject : null;
        }

        public bool tryGetInWorld(InstanceId id, [MaybeNullWhen(false)] out WorldObject worldObject) {
            worldObject = getInWorld(id);
            return worldObject != null;
        }

        public WorldObject? getInWorld(InstanceId id) {
            WorldObject? worldObject = get(id);
            return (worldObject != null && worldObject.inWorld) ? worldObject : null;
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
                if (!worldObject.deleted) {
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
                EntityDef entityDef = contentManager.getInheritedEntityDef(worldObject.entityDid);
                if (entityDef.type != EntityType.Weenie && entityDef.type != EntityType.EntityDesc) {
                    throw new ArgumentException(entityDef.type.ToString());
                }

                if (entityDef.bools != null) {
                    foreach ((PropertyName prop, bool value) in entityDef.bools) {
                        if (PropertyMapper.PROPERTY_NAME_TO_BOOL_STAT.TryGetValue(prop, out BoolStat stat)) {
                            if (worldObject.qualities.bools == null) {
                                worldObject.qualities.bools = new();
                            }
                            worldObject.qualities.bools[stat] = value;
                        }
                    }
                }
                if (entityDef.ints != null) {
                    foreach ((PropertyName prop, int value) in entityDef.ints) {
                        if (PropertyMapper.PROPERTY_NAME_TO_INT_STAT.TryGetValue(prop, out IntStat stat)) {
                            if (worldObject.qualities.ints == null) {
                                worldObject.qualities.ints = new();
                            }
                            worldObject.qualities.ints[stat] = value;
                        }
                    }
                }
                if (entityDef.floats != null) {
                    foreach ((PropertyName prop, float value) in entityDef.floats) {
                        if (PropertyMapper.PROPERTY_NAME_TO_FLOAT_STAT.TryGetValue(prop, out FloatStat stat)) {
                            if (worldObject.qualities.floats == null) {
                                worldObject.qualities.floats = new();
                            }
                            worldObject.qualities.floats[stat] = value;
                        }
                    }
                }
                if (entityDef.strings != null) {
                    foreach ((PropertyName prop, string value) in entityDef.strings) {
                        if (PropertyMapper.PROPERTY_NAME_TO_STRING_STAT.TryGetValue(prop, out StringStat stat)) {
                            if (worldObject.qualities.strings == null) {
                                worldObject.qualities.strings = new();
                            }
                            worldObject.qualities.strings[stat] = value;
                        }
                    }
                }
                if (entityDef.dids != null) {
                    foreach ((PropertyName prop, DataId value) in entityDef.dids) {
                        if (PropertyMapper.PROPERTY_NAME_TO_DATA_ID_STAT.TryGetValue(prop, out DataIdStat stat)) {
                            if (worldObject.qualities.dids == null) {
                                worldObject.qualities.dids = new();
                            }
                            worldObject.qualities.dids[stat] = value;
                        }
                    }
                }
                if (entityDef.stringInfos != null) {
                    // TODO: Need to do a deep copy of the StringInfos?
                    foreach ((PropertyName prop, StringInfo value) in entityDef.stringInfos) {
                        if (PropertyMapper.PROPERTY_NAME_TO_STRING_INFO_STAT.TryGetValue(prop, out StringInfoStat stat)) {
                            if (worldObject.qualities.stringInfos == null) {
                                worldObject.qualities.stringInfos = new();
                            }
                            worldObject.qualities.stringInfos[stat] = value;
                        }
                    }
                }
                if (entityDef.packageIds != null) {
                    // TODO: Is there a mapping for this?
                }
                if (entityDef.longs != null) {
                    foreach ((PropertyName prop, long value) in entityDef.longs) {
                        if (PropertyMapper.PROPERTY_NAME_TO_LONG_INT_STAT.TryGetValue(prop, out LongIntStat stat)) {
                            if (worldObject.qualities.longs == null) {
                                worldObject.qualities.longs = new();
                            }
                            worldObject.qualities.longs[stat] = value;
                        }
                    }
                }
                if (entityDef.poss != null) {
                    foreach ((PropertyName prop, Position value) in entityDef.poss) {
                        if (PropertyMapper.PROPERTY_NAME_TO_POSITION_STAT.TryGetValue(prop, out PositionStat stat)) {
                            if (worldObject.qualities.poss == null) {
                                worldObject.qualities.poss = new();
                            }
                            worldObject.qualities.poss[stat] = value;
                        }
                    }
                }

                DataId qualitiesDid = new(0x81000000 + worldObject.entityDid.id - DbTypeDef.TYPE_TO_DEF[DbType.ENTITYDESC].baseDid.id);
                CBaseQualities baseQualities = contentManager.getQualities(qualitiesDid);

                if (baseQualities.ints != null) {
                    foreach ((IntStat stat, int value) in baseQualities.ints) {
                        if (worldObject.qualities.ints == null) {
                            worldObject.qualities.ints = new();
                        }
                        worldObject.qualities.ints[stat] = value;
                    }
                }
                if (baseQualities.longs != null) {
                    foreach ((LongIntStat stat, long value) in baseQualities.longs) {
                        if (worldObject.qualities.longs == null) {
                            worldObject.qualities.longs = new();
                        }
                        worldObject.qualities.longs[stat] = value;
                    }
                }
                if (baseQualities.bools != null) {
                    foreach ((BoolStat stat, bool value) in baseQualities.bools) {
                        if (worldObject.qualities.bools == null) {
                            worldObject.qualities.bools = new();
                        }
                        worldObject.qualities.bools[stat] = value;
                    }
                }
                if (baseQualities.floats != null) {
                    foreach ((FloatStat stat, float value) in baseQualities.floats) {
                        if (worldObject.qualities.floats == null) {
                            worldObject.qualities.floats = new();
                        }
                        worldObject.qualities.floats[stat] = value;
                    }
                }
                if (baseQualities.doubles != null) {
                    foreach ((TimestampStat stat, double value) in baseQualities.doubles) {
                        if (worldObject.qualities.doubles == null) {
                            worldObject.qualities.doubles = new();
                        }
                        worldObject.qualities.doubles[stat] = value;
                    }
                }
                if (baseQualities.strings != null) {
                    foreach ((StringStat stat, string value) in baseQualities.strings) {
                        if (worldObject.qualities.strings == null) {
                            worldObject.qualities.strings = new();
                        }
                        worldObject.qualities.strings[stat] = value;
                    }
                }
                if (baseQualities.dids != null) {
                    foreach ((DataIdStat stat, DataId value) in baseQualities.dids) {
                        if (worldObject.qualities.dids == null) {
                            worldObject.qualities.dids = new();
                        }
                        worldObject.qualities.dids[stat] = value;
                    }
                }
                if (baseQualities.ids != null) {
                    foreach ((InstanceIdStat stat, InstanceId value) in baseQualities.ids) {
                        if (worldObject.qualities.ids == null) {
                            worldObject.qualities.ids = new();
                        }
                        worldObject.qualities.ids[stat] = value;
                    }
                }
                if (baseQualities.poss != null) {
                    foreach ((PositionStat stat, Position value) in baseQualities.poss) {
                        if (worldObject.qualities.poss == null) {
                            worldObject.qualities.poss = new();
                        }
                        worldObject.qualities.poss[stat] = value;
                    }
                }
                if (baseQualities.stringInfos != null) {
                    // TODO: Need to do a deep copy of the StringInfos?
                    foreach ((StringInfoStat stat, StringInfo value) in baseQualities.stringInfos) {
                        if (worldObject.qualities.stringInfos == null) {
                            worldObject.qualities.stringInfos = new();
                        }
                        worldObject.qualities.stringInfos[stat] = value;
                    }
                }
                if (baseQualities.packageIds != null) {
                    foreach ((uint stat, PackageId value) in baseQualities.packageIds) {
                        if (worldObject.qualities.packageIds == null) {
                            worldObject.qualities.packageIds = new();
                        }
                        worldObject.qualities.packageIds[stat] = value;
                    }
                }

                WeenieDesc baseWeenie = baseQualities.weenieDesc;
                worldObject.qualities.did = baseQualities.did;
                worldObject.packageType = entityDef.packageType;
                worldObject.name = baseWeenie.name;
                worldObject.pluralName = baseWeenie.pluralName;
                worldObject.iconDid = baseWeenie.iconDid;
                worldObject.containerId = baseWeenie.containerId;
                worldObject.equipperId = baseWeenie.wielderId;
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
                DbType dbType = DbTypeDef.getType(DbTypeDef.DatType.PORTAL, worldObject.physicsEntityDid);
                if (dbType == DbType.VISUAL_DESC) {
                    worldObject.visual.parentDid = worldObject.physicsEntityDid;
                } else if (dbType == DbType.ENTITYDESC) {
                    EntityDef physicsEntityDef = contentManager.getInheritedEntityDef(worldObject.physicsEntityDid);
                    if (physicsEntityDef.type != EntityType.Physics) {
                        throw new ArgumentException(physicsEntityDef.type.ToString());
                    }

                    worldObject.visual.parentDid = physicsEntityDef.dataId;
                } else {
                    throw new ArgumentException(dbType.ToString());
                }
            }
        }

        public void broadcastUpdates() {
            foreach (WorldObject worldObject in worldObjects.Values) {
                worldObject.broadcastUpdates();
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
