using AC2RE.Definitions;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;

namespace AC2RE.Server.Database {

    internal class WorldMySqlDatabase : BaseMySqlDatabase, IWorldDatabase {

        protected override string? databaseName => "ac2re_world";

        private Character mapCharacter(MySqlDataReader reader) {
            return new(new(reader.GetGuid("id")), reader.GetUInt32("sequence"), new(reader.GetGuid("accountId")), new(reader.GetUInt64("objectId")));
        }

        public Character? getCharacterWithId(CharacterId id) {
            using (MySqlCommand cmd = new($"SELECT * FROM characters WHERE id = '{id.id}';", connection)) {
                using (MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow)) {
                    if (reader.Read()) {
                        return mapCharacter(reader);
                    }
                }
            }

            return null;
        }

        public List<Character> getCharactersWithAccount(AccountId accountId) {
            List<Character> characters = new();
            using (MySqlCommand cmd = new($"SELECT * FROM characters WHERE accountId = '{accountId.id}';", connection)) {
                using (MySqlDataReader reader = cmd.ExecuteReader()) {
                    while (reader.Read()) {
                        characters.Add(mapCharacter(reader));
                    }
                }
            }

            return characters;
        }

        public InstanceIdGenerator? getInstanceIdGenerator(string type) {
            using (MySqlCommand cmd = new($"SELECT idCounter FROM id_gen WHERE type = '{type}';", connection)) {
                using (MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleResult)) {
                    if (reader.Read()) {
                        return new(type, reader.GetUInt64("idCounter"));
                    }
                }
            }

            return null;
        }

        private WorldObject mapWorldObject(World world, MySqlDataReader reader) {
            WorldObject worldObject = new(world, new(reader.GetUInt64("id")), true);
            worldObject.entityDid = new(reader.GetUInt32("entityDid"));
            worldObject.physicsEntityDid = new(reader.GetUInt32("physicsEntityDid"));
            return worldObject;
        }

        public List<WorldObject> getAllWorldObjects(World world) {
            return getWorldObjectsWhere(world);
        }

        public WorldObject? getWorldObjectWithId(World world, InstanceId id) {
            return getWorldObjectWhere(world, $"WHERE id = {id.id}");
        }

        public List<WorldObject> getWorldObjectsWithLandblockId(World world, LandblockId landblockId) {
            return getWorldObjectsWhere(world, $"JOIN world_obj_phys ON landblockId = {landblockId.id}");
        }

        public List<WorldObject> getWorldObjectsWithContainerId(World world, InstanceId containerId) {
            return getWorldObjectsWhere(world, $"JOIN world_obj_stat_id ON objectId = id AND stat = {(uint)InstanceIdStat.CONTAINER} AND value = {containerId.id}");
        }

        public List<WorldObject> getWorldObjectsWithEquipperId(World world, InstanceId equipperId) {
            return getWorldObjectsWhere(world, $"JOIN world_obj_stat_id ON objectId = id AND stat = {(uint)InstanceIdStat.EQUIPPER} AND value = {equipperId.id}");
        }

        public List<WorldObject> getWorldObjectsWithParentId(World world, InstanceId parentId) {
            return getWorldObjectsWhere(world, $"JOIN world_obj_phys ON parentId = {parentId.id}");
        }

        private WorldObject? getWorldObjectWhere(World world, string? whereClause = null) {
            using (MySqlCommand cmd = new($"SELECT * FROM world_obj {whereClause ?? ""};", connection)) {
                using (MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow)) {
                    if (reader.Read()) {
                        return mapWorldObject(world, reader);
                    }
                }
            }

            return null;
        }

        private List<WorldObject> getWorldObjectsWhere(World world, string? whereClause = null) {
            List<WorldObject> worldObjects = new();
            using (MySqlCommand cmd = new($"SELECT * FROM world_obj {whereClause ?? ""};", connection)) {
                using (MySqlDataReader reader = cmd.ExecuteReader()) {
                    while (reader.Read()) {
                        worldObjects.Add(mapWorldObject(world, reader));
                    }
                }
            }

            return worldObjects;
        }

        public void initWorldObject(World world, WorldObject worldObject) {
            world.objectManager.applyEntities(worldObject, worldObject.physicsEntityDid);

            using (MySqlCommand cmd = new($"SELECT * FROM world_obj_phys WHERE objectId = '{worldObject.id.id}';", connection)) {
                using (MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow)) {
                    if (reader.Read()) {
                        worldObject.physics.pos = mapPosition(reader);
                        worldObject.physics.headingX = reader.GetFloat("headingX");
                        worldObject.physics.headingZ = reader.GetFloat("headingZ");
                        worldObject.physics.parentId = new(reader.GetUInt64("parentId"));
                        worldObject.physics.parentInstanceStamp = reader.GetUInt16("parentInstanceStamp");
                        worldObject.physics.locationId = (HoldingLocation)reader.GetUInt32("locationId");
                        worldObject.physics.orientationId = (Orientation)reader.GetUInt32("orientationId");
                        worldObject.physics.instanceStamp = reader.GetUInt16("instanceStamp");
                    }
                }
            }

            using (MySqlCommand cmd = new($"SELECT * FROM world_obj_visual WHERE objectId = '{worldObject.id.id}';", connection)) {
                using (MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow)) {
                    if (reader.Read()) {
                        worldObject.visualScale = new(reader.GetFloat("scaleX"), reader.GetFloat("scaleY"), reader.GetFloat("scaleZ"));
                    }
                }
            }

            using (MySqlCommand cmd = new($"SELECT * FROM world_obj_apr WHERE objectId = '{worldObject.id.id}';", connection)) {
                using (MySqlDataReader reader = cmd.ExecuteReader()) {
                    while (reader.Read()) {
                        if (worldObject.globalAppearanceModifiers == null) {
                            worldObject.globalAppearanceModifiers = new();
                            worldObject.globalAppearanceModifiers.key = PartGroupDataDesc.PartGroupKey.ENTIRE_TREE;
                        }

                        if (worldObject.globalAppearanceModifiers.appearanceInfos == null) {
                            worldObject.globalAppearanceModifiers.appearanceInfos = new();
                        }

                        DataId partDid = new(reader.GetUInt32("partDid"));
                        if (!worldObject.globalAppearanceModifiers.appearanceInfos.TryGetValue(partDid, out Dictionary<AppearanceKey, float>? appearanceInfos)) {
                            appearanceInfos = new();
                            worldObject.globalAppearanceModifiers.appearanceInfos[partDid] = appearanceInfos;
                        }

                        appearanceInfos[(AppearanceKey)reader.GetUInt32("aprKey")] = reader.GetFloat("value");
                    }
                }
            }

            using (MySqlCommand cmd = new($"SELECT * FROM world_obj_stat_int WHERE objectId = '{worldObject.id.id}';", connection)) {
                using (MySqlDataReader reader = cmd.ExecuteReader()) {
                    while (reader.Read()) {
                        worldObject.setQ((IntStat)reader.GetUInt32("stat"), reader.GetInt32("value"));
                    }
                }
            }
            using (MySqlCommand cmd = new($"SELECT * FROM world_obj_stat_long WHERE objectId = '{worldObject.id.id}';", connection)) {
                using (MySqlDataReader reader = cmd.ExecuteReader()) {
                    while (reader.Read()) {
                        worldObject.setQ((LongIntStat)reader.GetUInt32("stat"), reader.GetInt64("value"));
                    }
                }
            }
            using (MySqlCommand cmd = new($"SELECT * FROM world_obj_stat_bool WHERE objectId = '{worldObject.id.id}';", connection)) {
                using (MySqlDataReader reader = cmd.ExecuteReader()) {
                    while (reader.Read()) {
                        worldObject.setQ((BoolStat)reader.GetUInt32("stat"), reader.GetBoolean("value"));
                    }
                }
            }
            using (MySqlCommand cmd = new($"SELECT * FROM world_obj_stat_float WHERE objectId = '{worldObject.id.id}';", connection)) {
                using (MySqlDataReader reader = cmd.ExecuteReader()) {
                    while (reader.Read()) {
                        worldObject.setQ((FloatStat)reader.GetUInt32("stat"), reader.GetFloat("value"));
                    }
                }
            }
            using (MySqlCommand cmd = new($"SELECT * FROM world_obj_stat_double WHERE objectId = '{worldObject.id.id}';", connection)) {
                using (MySqlDataReader reader = cmd.ExecuteReader()) {
                    while (reader.Read()) {
                        worldObject.setQ((TimestampStat)reader.GetUInt32("stat"), reader.GetDouble("value"));
                    }
                }
            }
            using (MySqlCommand cmd = new($"SELECT * FROM world_obj_stat_id WHERE objectId = '{worldObject.id.id}';", connection)) {
                using (MySqlDataReader reader = cmd.ExecuteReader()) {
                    while (reader.Read()) {
                        worldObject.setQ((InstanceIdStat)reader.GetUInt32("stat"), new(reader.GetUInt64("value")));
                    }
                }
            }
            using (MySqlCommand cmd = new($"SELECT * FROM world_obj_stat_did WHERE objectId = '{worldObject.id.id}';", connection)) {
                using (MySqlDataReader reader = cmd.ExecuteReader()) {
                    while (reader.Read()) {
                        worldObject.setQ((DataIdStat)reader.GetUInt32("stat"), new(reader.GetUInt32("value")));
                    }
                }
            }
            using (MySqlCommand cmd = new($"SELECT * FROM world_obj_stat_str WHERE objectId = '{worldObject.id.id}';", connection)) {
                using (MySqlDataReader reader = cmd.ExecuteReader()) {
                    while (reader.Read()) {
                        worldObject.setQ((StringStat)reader.GetUInt32("stat"), reader.GetString("value"));
                    }
                }
            }
            using (MySqlCommand cmd = new($"SELECT * FROM world_obj_stat_strinfo WHERE objectId = '{worldObject.id.id}';", connection)) {
                using (MySqlDataReader reader = cmd.ExecuteReader()) {
                    while (reader.Read()) {
                        worldObject.setQ((StringInfoStat)reader.GetUInt32("stat"), !reader.IsDBNull("literalValue") ? new(reader.GetString("literalValue")) : new(new(reader.GetUInt32("tableDid")), reader.GetUInt32("stringId")));
                    }
                }
            }

            worldObject.syncWeenieDesc();

            List<WorldObject> containedItems = world.objectManager.loadWithContainerId(worldObject.id);
            if (containedItems.Count > 0) {
                worldObject.recacheContain(containedItems);
            }

            List<WorldObject> equippedItems = world.objectManager.loadWithEquipperId(worldObject.id);
            if (equippedItems.Count > 0) {
                worldObject.recacheEquip(equippedItems);
            }

            List<WorldObject> childWorldObjects = world.objectManager.loadWithParentId(worldObject.id);
            if (childWorldObjects.Count > 0) {
                worldObject.recachePhysics(childWorldObjects);
            }
        }

        public bool save(WorldSave worldSave) {
            return runInTransaction(connection, transaction => {
                InstanceIdGenerator? idGenerator = worldSave.idGenerator;
                if (idGenerator != null) {
                    using (MySqlCommand cmd = upsertCommand(connection, transaction, "id_gen",
                        new("type", MySqlDbType.String),
                        new("idCounter", MySqlDbType.UInt64))) {
                        cmd.Parameters[0].Value = idGenerator.type;
                        cmd.Parameters[1].Value = idGenerator.idCounter;
                        cmd.ExecuteNonQuery();
                    }
                }

                using (MySqlCommand objCmd = upsertCommand(connection, transaction, "world_obj",
                    new("id", MySqlDbType.UInt64),
                    new("entityDid", MySqlDbType.UInt32),
                    new("physicsEntityDid", MySqlDbType.UInt32))) {
                    foreach (WorldObject worldObject in worldSave.worldObjects) {
                        objCmd.Parameters[0].Value = worldObject.id.id;
                        objCmd.Parameters[1].Value = worldObject.entityDid.id;
                        objCmd.Parameters[2].Value = worldObject.physicsEntityDid.id;
                        objCmd.ExecuteNonQuery();

                        if (worldObject.qualities.ints != null) {
                            using (MySqlCommand cmd = upsertCommand(connection, transaction, "world_obj_stat_int",
                            new("objectId", MySqlDbType.UInt64),
                            new("stat", MySqlDbType.UInt32),
                            new("value", MySqlDbType.Int32))) {
                                cmd.Parameters[0].Value = worldObject.id.id;
                                foreach ((IntStat stat, int value) in worldObject.qualities.ints) {
                                    cmd.Parameters[1].Value = (uint)stat;
                                    cmd.Parameters[2].Value = value;
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        if (worldObject.qualities.longs != null) {
                            using (MySqlCommand cmd = upsertCommand(connection, transaction, "world_obj_stat_long",
                            new("objectId", MySqlDbType.UInt64),
                            new("stat", MySqlDbType.UInt32),
                            new("value", MySqlDbType.Int64))) {
                                cmd.Parameters[0].Value = worldObject.id.id;
                                foreach ((LongIntStat stat, long value) in worldObject.qualities.longs) {
                                    cmd.Parameters[1].Value = (uint)stat;
                                    cmd.Parameters[2].Value = value;
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        if (worldObject.qualities.bools != null) {
                            using (MySqlCommand cmd = upsertCommand(connection, transaction, "world_obj_stat_bool",
                            new("objectId", MySqlDbType.UInt64),
                            new("stat", MySqlDbType.UInt32),
                            new("value", MySqlDbType.Bool))) {
                                cmd.Parameters[0].Value = worldObject.id.id;
                                foreach ((BoolStat stat, bool value) in worldObject.qualities.bools) {
                                    cmd.Parameters[1].Value = (uint)stat;
                                    cmd.Parameters[2].Value = value;
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        if (worldObject.qualities.floats != null) {
                            using (MySqlCommand cmd = upsertCommand(connection, transaction, "world_obj_stat_float",
                            new("objectId", MySqlDbType.UInt64),
                            new("stat", MySqlDbType.UInt32),
                            new("value", MySqlDbType.Float))) {
                                cmd.Parameters[0].Value = worldObject.id.id;
                                foreach ((FloatStat stat, float value) in worldObject.qualities.floats) {
                                    cmd.Parameters[1].Value = (uint)stat;
                                    cmd.Parameters[2].Value = value;
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        if (worldObject.qualities.doubles != null) {
                            using (MySqlCommand cmd = upsertCommand(connection, transaction, "world_obj_stat_double",
                                new("objectId", MySqlDbType.UInt64),
                                new("stat", MySqlDbType.UInt32),
                                new("value", MySqlDbType.Double))) {
                                cmd.Parameters[0].Value = worldObject.id.id;
                                foreach ((TimestampStat stat, double value) in worldObject.qualities.doubles) {
                                    cmd.Parameters[1].Value = (uint)stat;
                                    cmd.Parameters[2].Value = value;
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        if (worldObject.qualities.ids != null) {
                            using (MySqlCommand cmd = upsertCommand(connection, transaction, "world_obj_stat_id",
                            new("objectId", MySqlDbType.UInt64),
                            new("stat", MySqlDbType.UInt32),
                            new("value", MySqlDbType.UInt64))) {
                                cmd.Parameters[0].Value = worldObject.id.id;
                                foreach ((InstanceIdStat stat, InstanceId value) in worldObject.qualities.ids) {
                                    cmd.Parameters[1].Value = (uint)stat;
                                    cmd.Parameters[2].Value = value.id;
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        if (worldObject.qualities.dids != null) {
                            using (MySqlCommand cmd = upsertCommand(connection, transaction, "world_obj_stat_did",
                            new("objectId", MySqlDbType.UInt64),
                            new("stat", MySqlDbType.UInt32),
                            new("value", MySqlDbType.UInt32))) {
                                cmd.Parameters[0].Value = worldObject.id.id;
                                foreach ((DataIdStat stat, DataId value) in worldObject.qualities.dids) {
                                    cmd.Parameters[1].Value = (uint)stat;
                                    cmd.Parameters[2].Value = value.id;
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        if (worldObject.qualities.strings != null) {
                            using (MySqlCommand cmd = upsertCommand(connection, transaction, "world_obj_stat_str",
                            new("objectId", MySqlDbType.UInt64),
                            new("stat", MySqlDbType.UInt32),
                            new("value", MySqlDbType.VarString))) {
                                cmd.Parameters[0].Value = worldObject.id.id;
                                foreach ((StringStat stat, string value) in worldObject.qualities.strings) {
                                    cmd.Parameters[1].Value = (uint)stat;
                                    cmd.Parameters[2].Value = value;
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        if (worldObject.qualities.stringInfos != null) {
                            using (MySqlCommand cmd = upsertCommand(connection, transaction, "world_obj_stat_strinfo",
                            new("objectId", MySqlDbType.UInt64),
                            new("stat", MySqlDbType.UInt32),
                            new("stringId", MySqlDbType.UInt32),
                            new("tableDid", MySqlDbType.UInt32),
                            new("literalValue", MySqlDbType.VarString))) {
                                cmd.Parameters[0].Value = worldObject.id.id;
                                foreach ((StringInfoStat stat, StringInfo value) in worldObject.qualities.stringInfos) {
                                    cmd.Parameters[1].Value = (uint)stat;
                                    cmd.Parameters[2].Value = value.stringId;
                                    cmd.Parameters[3].Value = value.tableDid.id;
                                    cmd.Parameters[4].Value = value.literalValue;
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }

                        using (MySqlCommand cmd = upsertCommand(connection, transaction, "world_obj_phys",
                            new("objectId", MySqlDbType.UInt64),
                            new("landblockId", MySqlDbType.UInt16),
                            new("cellId", MySqlDbType.UInt16),
                            new("posX", MySqlDbType.Float),
                            new("posY", MySqlDbType.Float),
                            new("posZ", MySqlDbType.Float),
                            new("rotX", MySqlDbType.Float),
                            new("rotY", MySqlDbType.Float),
                            new("rotZ", MySqlDbType.Float),
                            new("rotW", MySqlDbType.Float),
                            new("headingX", MySqlDbType.Float),
                            new("headingZ", MySqlDbType.Float),
                            new("parentId", MySqlDbType.UInt64),
                            new("parentInstanceStamp", MySqlDbType.UInt16),
                            new("locationId", MySqlDbType.UInt32),
                            new("orientationId", MySqlDbType.UInt32),
                            new("instanceStamp", MySqlDbType.UInt16))) {
                            cmd.Parameters[0].Value = worldObject.id.id;
                            cmd.Parameters[1].Value = worldObject.physics.pos.cell.landblockId.id;
                            cmd.Parameters[2].Value = worldObject.physics.pos.cell.localCellId.id;
                            cmd.Parameters[3].Value = worldObject.physics.pos.frame.pos.X;
                            cmd.Parameters[4].Value = worldObject.physics.pos.frame.pos.Y;
                            cmd.Parameters[5].Value = worldObject.physics.pos.frame.pos.Z;
                            cmd.Parameters[6].Value = worldObject.physics.pos.frame.rot.X;
                            cmd.Parameters[7].Value = worldObject.physics.pos.frame.rot.Y;
                            cmd.Parameters[8].Value = worldObject.physics.pos.frame.rot.Z;
                            cmd.Parameters[9].Value = worldObject.physics.pos.frame.rot.W;
                            cmd.Parameters[10].Value = worldObject.physics.headingX;
                            cmd.Parameters[11].Value = worldObject.physics.headingZ;
                            cmd.Parameters[12].Value = worldObject.physics.parentId.id;
                            cmd.Parameters[13].Value = worldObject.physics.parentInstanceStamp;
                            cmd.Parameters[14].Value = worldObject.physics.locationId;
                            cmd.Parameters[15].Value = worldObject.physics.orientationId;
                            cmd.Parameters[16].Value = worldObject.physics.instanceStamp;
                            cmd.ExecuteNonQuery();
                        }

                        using (MySqlCommand cmd = upsertCommand(connection, transaction, "world_obj_visual",
                            new("objectId", MySqlDbType.UInt64),
                            new("scaleX", MySqlDbType.Float),
                            new("scaleY", MySqlDbType.Float),
                            new("scaleZ", MySqlDbType.Float))) {
                            cmd.Parameters[0].Value = worldObject.id.id;
                            cmd.Parameters[1].Value = worldObject.visualScale.X;
                            cmd.Parameters[2].Value = worldObject.visualScale.Y;
                            cmd.Parameters[3].Value = worldObject.visualScale.Z;
                            cmd.ExecuteNonQuery();
                        }

                        if (worldObject.globalAppearanceModifiers != null) {
                            using (MySqlCommand cmd = upsertCommand(connection, transaction, "world_obj_apr",
                                new("objectId", MySqlDbType.UInt64),
                                new("partDid", MySqlDbType.UInt32),
                                new("aprKey", MySqlDbType.UInt32),
                                new("value", MySqlDbType.Float))) {
                                cmd.Parameters[0].Value = worldObject.id.id;
                                foreach ((DataId partDid, Dictionary<AppearanceKey, float> appearances) in worldObject.globalAppearanceModifiers.appearanceInfos) {
                                    cmd.Parameters[1].Value = partDid.id;
                                    foreach ((AppearanceKey aprKey, float value) in appearances) {
                                        cmd.Parameters[2].Value = (uint)aprKey;
                                        cmd.Parameters[3].Value = value;
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }
                }

                using (MySqlCommand cmd = upsertCommand(connection, transaction, "characters",
                    new("id", MySqlDbType.String),
                    new("sequence", MySqlDbType.UInt16),
                    new("accountId", MySqlDbType.String),
                    new("objectId", MySqlDbType.UInt64))) {
                    foreach (Character character in worldSave.characters) {
                        cmd.Parameters[0].Value = character.id.id;
                        cmd.Parameters[1].Value = character.sequence;
                        cmd.Parameters[2].Value = character.accountId.id;
                        cmd.Parameters[3].Value = character.objectId.id;
                        cmd.ExecuteNonQuery();
                    }
                }

                return true;
            });
        }
    }
}
