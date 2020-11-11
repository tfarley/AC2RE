using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;

namespace AC2E.Server.Database {

    internal class UInt32SafeSerializer : UInt32Serializer {

        public UInt32SafeSerializer() : base(BsonType.Int32, new(true, false)) {

        }

        public UInt32SafeSerializer(BsonType representation) : base(representation, new(true, false)) {

        }
    }
}
