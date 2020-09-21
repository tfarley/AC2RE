﻿using AC2E.Def;
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

                BsonSerializer.RegisterSerializer(new GuidIdSerializer<AccountId>(id => new AccountId(id), v => v.id));
                BsonSerializer.RegisterSerializer(new GuidIdSerializer<CharacterId>(id => new CharacterId(id), v => v.id));
                BsonSerializer.RegisterSerializer(new QuaternionSerializer());
                BsonSerializer.RegisterSerializer(new StringInfoSerializer());
                BsonSerializer.RegisterSerializer(new UInt32IdSerializer<CellId>(id => new CellId(id), v => v.id));
                BsonSerializer.RegisterSerializer(new UInt32IdSerializer<DataId>(id => new DataId(id), v => v.id));
                BsonSerializer.RegisterSerializer(new UInt32IdSerializer<PackageId>(id => new PackageId(id), v => v.id));
                BsonSerializer.RegisterSerializer(new UInt64IdSerializer<InstanceId>(id => new InstanceId(id), v => v.id));
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
