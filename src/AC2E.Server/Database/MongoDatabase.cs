using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System.Collections.Generic;

namespace AC2E.Server.Database {

    internal abstract class MongoDatabase {

        private static bool mongoInited;
        private static Dictionary<string, MongoClient> endpointToClient = new Dictionary<string, MongoClient>();

        protected abstract string databaseName { get; }

        protected readonly MongoClient client;
        protected readonly IMongoDatabase database;

        public MongoDatabase(string endpoint) {
            if (!mongoInited) {
                ConventionRegistry.Register(
                    "GlobalConventions",
                    new ConventionPack {
                        new IgnoreExtraElementsConvention(true)
                    },
                    type => true);

                BsonSerializer.RegisterSerializer(new DataIdSerializer());
                BsonSerializer.RegisterSerializer(new InstanceIdSerializer());
                BsonSerializer.RegisterSerializer(new StringInfoSerializer());
                BsonSerializer.RegisterSerializer(new Vector3Serializer());

                mongoInited = true;
            }

            if (!endpointToClient.TryGetValue(endpoint, out client)) {
                client = new MongoClient(endpoint);
                endpointToClient[endpoint] = client;
            }

            database = client.GetDatabase(databaseName);
        }
    }
}
