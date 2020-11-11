using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;

namespace AC2E.Server.Database {

    internal class UInt64SafeSerializer : UInt64Serializer {

        public UInt64SafeSerializer() : base(BsonType.Int64, new(true, false)) {

        }

        public UInt64SafeSerializer(BsonType representation) : base(representation, new(true, false)) {

        }
    }
}
