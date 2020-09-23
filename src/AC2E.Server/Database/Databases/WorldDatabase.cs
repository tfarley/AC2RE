using AC2E.Def;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System.Collections.Generic;

namespace AC2E.Server.Database {

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
                BsonClassMap.RegisterClassMap<Character>(c => {
                    c.AutoMap();
                    c.MapCreator(r => new Character(r.id));
                });
            }

            IMongoCollection<Character> characters = database.GetCollection<Character>("character");

            if (!inited) {
                characters.Indexes.CreateOne(new CreateIndexModel<Character>(
                    Builders<Character>.IndexKeys.Ascending(r => r.worldObjectId),
                    new CreateIndexOptions<Character>() { Unique = true }));
            }

            return characters;
        }

        private IMongoCollection<InstanceIdGenerator> setupInstanceIdGenerators() {
            if (!inited) {
                BsonClassMap.RegisterClassMap<InstanceIdGenerator>(c => {
                    c.MapIdField(r => r.type);
                    c.MapCreator(r => new InstanceIdGenerator(r.type, r.idCounter));
                    c.MapField(r => r.idCounter);
                });
            }

            IMongoCollection<InstanceIdGenerator> instanceIdGenerators = database.GetCollection<InstanceIdGenerator>("id_gen");

            return instanceIdGenerators;
        }

        private IMongoCollection<WorldObject> setupWorldObjects() {
            if (!inited) {
                BsonClassMap.RegisterClassMap<WorldObject>(c => {
                    c.AutoMap();
                    c.MapIdField(r => r.id);
                    c.MapCreator(r => new WorldObject(r.id));
                    c.UnmapField(r => r.instanceStamp);
                });

                BsonClassMap.RegisterClassMap<PhysicsDesc>(c => {
                    c.AutoMap();
                    c.MapField(r => r.sliders).SetSerializer(new DictionaryInterfaceImplementerSerializer<Dictionary<uint, PhysicsDesc.SliderData>>()
                        .WithKeySerializer(new UInt32SafeSerializer(BsonType.String))
                        .WithValueSerializer(new BsonClassMapSerializer<PhysicsDesc.SliderData>(new BsonClassMap<PhysicsDesc.SliderData>().Freeze())));
                    c.MapField(r => r.fx).SetSerializer(new DictionaryInterfaceImplementerSerializer<Dictionary<uint, FXScalarAndTarget>>()
                        .WithKeySerializer(new UInt32SafeSerializer(BsonType.String))
                        .WithValueSerializer(new BsonClassMapSerializer<FXScalarAndTarget>(new BsonClassMap<FXScalarAndTarget>().Freeze())));
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
                        .WithKeySerializer(new UInt32IdSerializer<DataId>(id => new DataId(id), v => v.id, BsonType.String))
                        .WithValueSerializer(new DictionaryInterfaceImplementerSerializer<Dictionary<AppearanceKey, float>>()
                            .WithKeySerializer(new UInt32SafeSerializer(BsonType.String))
                            .WithValueSerializer(new SingleSerializer())));
                    c.MapField(r => r.startupFx).SetSerializer(new DictionaryInterfaceImplementerSerializer<Dictionary<uint, float>>()
                        .WithKeySerializer(new UInt32SafeSerializer(BsonType.String))
                        .WithValueSerializer(new SingleSerializer()));
                });

                BsonClassMap.RegisterClassMap<IconDesc>();
                BsonClassMap.RegisterClassMap<IconLayerDesc>();

                BsonClassMap.RegisterClassMap<WeenieDesc>();
            }

            IMongoCollection<WorldObject> worldObjects = database.GetCollection<WorldObject>("worldobj");

            return worldObjects;
        }

        public Character getCharacterWithId(CharacterId id) {
            return characters.Find(r => !r.deleted && r.id == id).FirstOrDefault();
        }

        public List<Character> getCharactersWithAccount(AccountId accountId) {
            return characters.Find(r => !r.deleted && r.ownerAccountId == accountId).ToList();
        }

        public InstanceIdGenerator getInstanceIdGenerator(string type) {
            return instanceIdGenerators.Find(r => r.type == type).FirstOrDefault();
        }

        public List<WorldObject> getWorldObjectsInWorld() {
            return worldObjects.Find(r => !r.deleted && r.inWorld).ToList();
        }

        public WorldObject getWorldObjectWithId(InstanceId id) {
            return worldObjects.Find(r => !r.deleted && r.id == id).FirstOrDefault();
        }

        public List<WorldObject> getWorldObjectsWithContainer(InstanceId containerId) {
            return worldObjects.Find(r => !r.deleted && r.weenie.containerId == containerId).ToList();
        }

        public bool save(WorldSave worldSave) {
            using (var session = client.StartSession()) {
                // TODO: Define TransactionOptions?
                return session.WithTransaction((s, ct) => {
                    // TODO: Needed individual replace calls here because ReplaceOneModel in BulkWrite does not support Upsert, and the alternative requires specifying every field in UpdateOneModel
                    foreach (Character character in worldSave.characters) {
                        characters.ReplaceOne(session, r => r.id == character.id, character, new ReplaceOptions() { IsUpsert = true }, ct);
                    }

                    instanceIdGenerators.UpdateOne(
                        session,
                        r => r.type == worldSave.idGenerator.type,
                        Builders<InstanceIdGenerator>.Update
                            .SetOnInsert(r => r.type, worldSave.idGenerator.type)
                            .Set(r => r.idCounter, worldSave.idGenerator.idCounter),
                        new UpdateOptions() { IsUpsert = true },
                        ct);

                    // TODO: Needed individual replace calls here because ReplaceOneModel in BulkWrite does not support Upsert, and the alternative requires specifying every field in UpdateOneModel
                    foreach (WorldObject worldObject in worldSave.worldObjects) {
                        worldObjects.ReplaceOne(session, r => r.id == worldObject.id, worldObject, new ReplaceOptions() { IsUpsert = true }, ct);
                    }

                    return true;
                });
            }
        }
    }
}
