using AC2RE.Definitions;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Reflection;

namespace AC2RE.Server.Database {

    internal abstract class MongoDatabase {

        public static readonly IConventionPack GLOBAL_CONVENTIONS = new ConventionPack {
            new DatabaseAttributesConvention(),
            new IgnoreIfDefaultConvention(true),
            new IgnoreExtraElementsConvention(true),
        };

        private static bool mongoInited;
        private static readonly Dictionary<string, MongoClient> endpointToClient = new();

        protected abstract string databaseName { get; }

        protected readonly MongoClient client;
        protected readonly IMongoDatabase database;

        public MongoDatabase(string endpoint) {
            if (!mongoInited) {
                ConventionRegistry.Register(
                    "GlobalConventions",
                    GLOBAL_CONVENTIONS,
                    type => true);

                ConventionRegistry.Register(
                    "ExternalConventions",
                    new ConventionPack {
                        // Serialize private fields and properties by default
                        new ReadWriteMemberFinderConvention(MemberTypes.All, (BindingFlags)(-1)),
                    },
                    type => true);

                BsonSerializer.RegisterSerializationProvider(new SerializationProvider());

                BsonSerializer.RegisterSerializer(new FrameSerializer());
                BsonSerializer.RegisterSerializer(new GuidIdSerializer<AccountId>(id => new(id), v => v.id));
                BsonSerializer.RegisterSerializer(new GuidIdSerializer<CharacterId>(id => new(id), v => v.id));
                BsonSerializer.RegisterSerializer(new QuaternionSerializer());
                BsonSerializer.RegisterSerializer(new RGBAColorSerializer());
                BsonSerializer.RegisterSerializer(new StringInfoSerializer());
                BsonSerializer.RegisterSerializer(new UInt32IdSerializer<LandblockId>(id => new((ushort)id), v => v.id));
                BsonSerializer.RegisterSerializer(new UInt32IdSerializer<CellId>(id => new(id), v => v.id));
                BsonSerializer.RegisterSerializer(new UInt32IdSerializer<DataId>(id => new(id), v => v.id));
                BsonSerializer.RegisterSerializer(new UInt32IdSerializer<PackageId>(id => new(id), v => v.id));
                BsonSerializer.RegisterSerializer(new UInt64IdSerializer<InstanceId>(id => new(id), v => v.id));
                BsonSerializer.RegisterSerializer(new Vector3Serializer());

                BsonClassMap.RegisterClassMap<Position>();

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
