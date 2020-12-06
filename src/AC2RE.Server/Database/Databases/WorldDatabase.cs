﻿using AC2RE.Definitions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System.Collections.Generic;

namespace AC2RE.Server.Database {

    internal class WorldDatabase : MongoDatabase {

        protected override string databaseName => "world";

        private static bool inited;
        private readonly IMongoCollection<Character> characters;
        private readonly IMongoCollection<InstanceIdGenerator> instanceIdGenerators;
        private readonly IMongoCollection<WorldObject> worldObjects;

        public WorldDatabase(string endpoint) : base(endpoint) {
            characters = setupCharacters();
            instanceIdGenerators = setupInstanceIdGenerators();
            worldObjects = setupWorldObjects();
            inited = true;
        }

        private IMongoCollection<Character> setupCharacters() {
            if (!inited) {
                BsonClassMap.RegisterClassMap<Character>(BsonUtil.applyGlobalConventions);
            }

            IMongoCollection<Character> characters = database.GetCollection<Character>("character");

            if (!inited) {
                characters.Indexes.CreateOne(new CreateIndexModel<Character>(
                    Builders<Character>.IndexKeys.Ascending(r => r.objectId),
                    new() { Unique = true }));
            }

            return characters;
        }

        private IMongoCollection<InstanceIdGenerator> setupInstanceIdGenerators() {
            if (!inited) {
                BsonClassMap.RegisterClassMap<InstanceIdGenerator>(BsonUtil.applyGlobalConventions);
            }

            IMongoCollection<InstanceIdGenerator> instanceIdGenerators = database.GetCollection<InstanceIdGenerator>("id_gen");

            return instanceIdGenerators;
        }

        private IMongoCollection<WorldObject> setupWorldObjects() {
            if (!inited) {
                BsonClassMap.RegisterClassMap<WorldObject>(c => {
                    BsonUtil.applyGlobalConventions(c);
                    c.MapField("invLocToEquippedItemId").SetSerializer(new DictionaryInterfaceImplementerSerializer<Dictionary<InvLoc, InstanceId>>()
                        .WithKeySerializer(new UInt32SafeSerializer(BsonType.String))
                        .WithValueSerializer(new UInt64IdSerializer<InstanceId>(id => new(id), v => v.id)));
                });

                BsonClassMap.RegisterClassMap<PhysicsDesc.SliderData>();
                BsonClassMap.RegisterClassMap<FXScalarAndTarget>();

                BsonClassMap.RegisterClassMap<PhysicsDesc>(c => {
                    c.AutoMap();
                    c.MapField(r => r.sliders).SetSerializer(new DictionaryInterfaceImplementerSerializer<Dictionary<uint, PhysicsDesc.SliderData>>()
                        .WithKeySerializer(new UInt32SafeSerializer(BsonType.String))
                        .WithValueSerializer(BsonUtil.existingClassSerializer<PhysicsDesc.SliderData>()));
                    c.MapField(r => r.fx).SetSerializer(new DictionaryInterfaceImplementerSerializer<Dictionary<FxId, FXScalarAndTarget>>()
                        .WithKeySerializer(new UInt32SafeSerializer(BsonType.String))
                        .WithValueSerializer(BsonUtil.existingClassSerializer<FXScalarAndTarget>()));
                });

                BsonClassMap.RegisterClassMap<BehaviorParams>(c => {
                    c.AutoMap();
                    c.MapField(r => r.clonedAppHash).SetSerializer(new DictionaryInterfaceImplementerSerializer<Dictionary<AppearanceKey, float>>()
                        .WithKeySerializer(new UInt32SafeSerializer(BsonType.String))
                        .WithValueSerializer(new SingleSerializer()));
                });

                BsonClassMap.RegisterClassMap<VisualDesc>(c => {
                    c.AutoMap();
                    c.MapField(r => r.pgdDescTable).SetSerializer(new DictionaryInterfaceImplementerSerializer<Dictionary<uint, PartGroupDataDesc>>()
                        .WithKeySerializer(new UInt32SafeSerializer(BsonType.String)));
                });

                BsonClassMap.RegisterClassMap<PartGroupDataDesc>(c => {
                    c.AutoMap();
                    c.UnmapField(r => r.fxOverrides); // TODO: Serialize this
                    c.MapField(r => r.appearanceInfos).SetSerializer(new DictionaryInterfaceImplementerSerializer<Dictionary<DataId, Dictionary<AppearanceKey, float>>>()
                        .WithKeySerializer(new UInt32IdSerializer<DataId>(id => new(id), v => v.id, BsonType.String))
                        .WithValueSerializer(new DictionaryInterfaceImplementerSerializer<Dictionary<AppearanceKey, float>>()
                            .WithKeySerializer(new UInt32SafeSerializer(BsonType.String))
                            .WithValueSerializer(new SingleSerializer())));
                    c.MapField(r => r.startupFx).SetSerializer(new DictionaryInterfaceImplementerSerializer<Dictionary<FxId, float>>()
                        .WithKeySerializer(new UInt32SafeSerializer(BsonType.String))
                        .WithValueSerializer(new SingleSerializer()));
                });

                BsonClassMap.RegisterClassMap<IconDesc>();
                BsonClassMap.RegisterClassMap<IconLayerDesc>();

                BsonClassMap.RegisterClassMap<CBaseQualities>(c => {
                    c.AutoMap();
                    c.MapField(r => r.ints).SetSerializer(new DictionaryInterfaceImplementerSerializer<Dictionary<IntStat, int>>()
                        .WithKeySerializer(new UInt32SafeSerializer(BsonType.String)));
                    c.MapField(r => r.longs).SetSerializer(new DictionaryInterfaceImplementerSerializer<Dictionary<LongIntStat, long>>()
                        .WithKeySerializer(new UInt32SafeSerializer(BsonType.String)));
                    c.MapField(r => r.bools).SetSerializer(new DictionaryInterfaceImplementerSerializer<Dictionary<BoolStat, bool>>()
                        .WithKeySerializer(new UInt32SafeSerializer(BsonType.String)));
                    c.MapField(r => r.floats).SetSerializer(new DictionaryInterfaceImplementerSerializer<Dictionary<FloatStat, float>>()
                        .WithKeySerializer(new UInt32SafeSerializer(BsonType.String))
                        .WithValueSerializer(new SingleSerializer()));
                    c.MapField(r => r.doubles).SetSerializer(new DictionaryInterfaceImplementerSerializer<Dictionary<TimestampStat, double>>()
                        .WithKeySerializer(new UInt32SafeSerializer(BsonType.String)));
                    c.MapField(r => r.strings).SetSerializer(new DictionaryInterfaceImplementerSerializer<Dictionary<StringStat, string>>()
                        .WithKeySerializer(new UInt32SafeSerializer(BsonType.String)));
                    c.MapField(r => r.dids).SetSerializer(new DictionaryInterfaceImplementerSerializer<Dictionary<DataIdStat, DataId>>()
                        .WithKeySerializer(new UInt32SafeSerializer(BsonType.String))
                        .WithValueSerializer(new UInt32IdSerializer<DataId>(id => new(id), v => v.id)));
                    c.MapField(r => r.ids).SetSerializer(new DictionaryInterfaceImplementerSerializer<Dictionary<InstanceIdStat, InstanceId>>()
                        .WithKeySerializer(new UInt32SafeSerializer(BsonType.String))
                        .WithValueSerializer(new UInt64IdSerializer<InstanceId>(id => new(id), v => v.id)));
                    c.MapField(r => r.poss).SetSerializer(new DictionaryInterfaceImplementerSerializer<Dictionary<PositionStat, Position>>()
                        .WithKeySerializer(new UInt32SafeSerializer(BsonType.String))
                        .WithValueSerializer(BsonUtil.existingClassSerializer<Position>()));
                    c.MapField(r => r.stringInfos).SetSerializer(new DictionaryInterfaceImplementerSerializer<Dictionary<StringInfoStat, StringInfo>>()
                        .WithKeySerializer(new UInt32SafeSerializer(BsonType.String))
                        .WithValueSerializer(new StringInfoSerializer()));
                    c.MapField(r => r.packageIds).SetSerializer(new DictionaryInterfaceImplementerSerializer<Dictionary<uint, PackageId>>()
                        .WithKeySerializer(new UInt32SafeSerializer(BsonType.String))
                        .WithValueSerializer(new UInt32IdSerializer<PackageId>(id => new(id), v => v.id)));
                });

                BsonClassMap.RegisterClassMap<WeenieDesc>();
            }

            IMongoCollection<WorldObject> worldObjects = database.GetCollection<WorldObject>("world_obj");

            if (!inited) {
                worldObjects.Indexes.CreateOne(new CreateIndexModel<WorldObject>(
                    Builders<WorldObject>.IndexKeys.Ascending(r => r.landblockId)));
            }

            return worldObjects;
        }

        public Character? getCharacterWithId(CharacterId id) {
            return characters.Find(
                r => !r.deleted
                && r.id == id
                ).FirstOrDefault();
        }

        public List<Character> getCharactersWithAccount(AccountId accountId) {
            return characters.Find(
                r => !r.deleted
                && r.accountId == accountId
                ).ToList();
        }

        public InstanceIdGenerator? getInstanceIdGenerator(string type) {
            return instanceIdGenerators.Find(
                r => r.type == type
                ).FirstOrDefault();
        }

        public List<WorldObject> getAllWorldObjects() {
            return worldObjects.Find(
                r => !r.destroyed
                ).ToList();
        }

        public WorldObject? getWorldObjectWithId(InstanceId id) {
            return worldObjects.Find(
                r => !r.destroyed
                && r.id == id
                ).FirstOrDefault();
        }

        public List<WorldObject> getWorldObjectsWithLandblockId(LandblockId landblockId) {
            return worldObjects.Find(
                r => !r.destroyed
                && r.landblockId == landblockId
                ).ToList();
        }

        public bool save(WorldSave worldSave) {
            using (var session = client.StartSession()) {
                // TODO: Define TransactionOptions?
                return session.WithTransaction((s, ct) => {
                    // TODO: Needed individual replace calls here because ReplaceOneModel in BulkWrite does not support Upsert, and the alternative requires specifying every field in UpdateOneModel
                    foreach (Character character in worldSave.characters) {
                        characters.ReplaceOne(session, r => r.id == character.id, character, new ReplaceOptions { IsUpsert = true }, ct);
                    }

                    InstanceIdGenerator? idGenerator = worldSave.idGenerator;
                    if (idGenerator != null) {
                        instanceIdGenerators.UpdateOne(
                            session,
                            r => r.type == idGenerator.type,
                            Builders<InstanceIdGenerator>.Update
                                .SetOnInsert(r => r.type, idGenerator.type)
                                .Set(r => r.idCounter, idGenerator.idCounter),
                            new() { IsUpsert = true },
                            ct);
                    }

                    // TODO: Needed individual replace calls here because ReplaceOneModel in BulkWrite does not support Upsert, and the alternative requires specifying every field in UpdateOneModel
                    foreach (WorldObject worldObject in worldSave.worldObjects) {
                        worldObjects.ReplaceOne(session, r => r.id == worldObject.id, worldObject, new ReplaceOptions { IsUpsert = true }, ct);
                    }

                    return true;
                });
            }
        }
    }
}
