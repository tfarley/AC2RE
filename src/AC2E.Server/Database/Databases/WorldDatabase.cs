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
        private readonly IMongoCollection<InstanceIdGenerator> instanceIdGenerators;
        private readonly IMongoCollection<WorldObject> worldObjects;
        private readonly HashSet<InstanceId> deletedWorldObjectIds = new HashSet<InstanceId>();

        public WorldDatabase(string endpoint) : base(endpoint) {
            instanceIdGenerators = setupInstanceIdGenerators();
            worldObjects = setupWorldObjects();
            inited = true;
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
                    //c.AutoMap();
                    c.MapIdMember(r => r.id);
                    c.MapCreator(r => new WorldObject(r.id, 0));
                    c.MapField(r => r.name);//.SetSerializer(new StringInfoLiteralSerializer());
                    c.MapField(r => r.visual);
                });

                BsonClassMap.RegisterClassMap<VisualComponent>(c => {
                    //c.AutoMap();
                    c.MapField(r => r.visualDesc);
                });

                BsonClassMap.RegisterClassMap<VisualDesc>(c => {
                    c.AutoMap();
                    c.UnmapField(r => r.packFlags);
                    c.UnmapField(r => r.iconDesc);
                    c.MapField(r => r.pgdDescTable).SetSerializer(new DictionaryInterfaceImplementerSerializer<Dictionary<uint, PartGroupDataDesc>>()
                        .WithKeySerializer(new UInt32Serializer(BsonType.String)));
                });

                BsonClassMap.RegisterClassMap<PartGroupDataDesc>(c => {
                    c.AutoMap();
                    c.UnmapField(r => r.packFlags);
                    c.UnmapField(r => r.fxOverrides);
                    c.MapField(r => r.appearanceInfos).SetSerializer(new DictionaryInterfaceImplementerSerializer<Dictionary<DataId, Dictionary<AppearanceKey, float>>>()
                        .WithKeySerializer(new DataIdSerializer(BsonType.String))
                        .WithValueSerializer(new DictionaryInterfaceImplementerSerializer<Dictionary<AppearanceKey, float>>()
                            .WithKeySerializer(new EnumSerializer<AppearanceKey>(BsonType.String))
                            .WithValueSerializer(new SingleSerializer())));
                });
            }

            IMongoCollection<WorldObject> worldObjects = database.GetCollection<WorldObject>("worldobj");

            return worldObjects;
        }

        public InstanceIdGenerator getInstanceIdGenerator(string type) {
            return instanceIdGenerators.Find(r => r.type == type).FirstOrDefault();
        }

        public List<WorldObject> getWorldObjects() {
            return worldObjects.Find(FilterDefinition<WorldObject>.Empty).ToList();
        }

        public WorldObject getWorldObjectWithId(InstanceId id) {
            return worldObjects.Find(r => r.id == id).FirstOrDefault();
        }

        public bool saveWorld(InstanceIdGenerator idGenerator, IEnumerable<WorldObject> worldObjects) {
            using (var session = client.StartSession()) {
                // TODO: Define TransactionOptions?
                return session.WithTransaction((s, ct) => {
                    if (deletedWorldObjectIds.Count > 0 && this.worldObjects.DeleteMany(r => deletedWorldObjectIds.Contains(r.id), ct).DeletedCount != deletedWorldObjectIds.Count) {
                        s.AbortTransaction();
                        return false;
                    }

                    instanceIdGenerators.UpdateOne(
                        r => r.type == idGenerator.type,
                        Builders<InstanceIdGenerator>.Update
                            .SetOnInsert(r => r.type, idGenerator.type)
                            .Set(r => r.idCounter, idGenerator.idCounter),
                        new UpdateOptions() { IsUpsert = true },
                        ct);

                    // TODO: Needed individual replace calls here because ReplaceOneModel in BulkWrite does not support Upsert, and the alternative requires specifying every field in UpdateOneModel
                    foreach (WorldObject worldObject in worldObjects) {
                        this.worldObjects.ReplaceOne(r => r.id == worldObject.id, worldObject, new ReplaceOptions() { IsUpsert = true }, ct);
                    }

                    return true;
                });
            }
        }

        public void enqueueDeleteWorldObject(InstanceId id) {
            deletedWorldObjectIds.Add(id);
        }
    }
}
