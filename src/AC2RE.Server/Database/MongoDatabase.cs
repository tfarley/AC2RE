using AC2RE.Definitions;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System.Collections.Generic;

namespace AC2RE.Server.Database {

    internal abstract class MongoDatabase {

        private static bool mongoInited;
        private static readonly Dictionary<string, MongoClient> endpointToClient = new();

        protected abstract string databaseName { get; }

        protected readonly MongoClient client;
        protected readonly IMongoDatabase database;

        public MongoDatabase(string endpoint) {
            if (!mongoInited) {
                ConventionRegistry.Register(
                    "GlobalConventions",
                    new ConventionPack {
                        new DatabaseIgnoreConvention(),
                        new IgnoreIfDefaultConvention(true),
                        new IgnoreExtraElementsConvention(true),
                    },
                    type => true);

                BsonSerializer.RegisterSerializationProvider(new SerializationProvider());

                BsonSerializer.RegisterSerializer(new GuidIdSerializer<AccountId>(id => new(id), v => v.id));
                BsonSerializer.RegisterSerializer(new GuidIdSerializer<CharacterId>(id => new(id), v => v.id));
                BsonSerializer.RegisterSerializer(new QuaternionSerializer());
                BsonSerializer.RegisterSerializer(new RGBAColorSerializer());
                BsonSerializer.RegisterSerializer(new StringInfoSerializer());
                BsonSerializer.RegisterSerializer(new UInt32IdSerializer<CellId>(id => new(id), v => v.id));
                BsonSerializer.RegisterSerializer(new UInt32IdSerializer<DataId>(id => new(id), v => v.id));
                BsonSerializer.RegisterSerializer(new UInt32IdSerializer<PackageId>(id => new(id), v => v.id));
                BsonSerializer.RegisterSerializer(new UInt64IdSerializer<InstanceId>(id => new(id), v => v.id));
                BsonSerializer.RegisterSerializer(new Vector3Serializer());

                BsonClassMap.RegisterClassMap<Position>();
                BsonClassMap.RegisterClassMap<Frame>();

                mongoInited = true;
            }

            if (!endpointToClient.TryGetValue(endpoint, out client!)) {
                client = new(endpoint);
                endpointToClient[endpoint] = client;
            }

            database = client.GetDatabase(databaseName);
        }
    }
}
