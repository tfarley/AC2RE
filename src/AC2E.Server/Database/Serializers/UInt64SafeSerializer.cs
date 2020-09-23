using MongoDB.Bson;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;

namespace AC2E.Server.Database {

    internal class UInt64SafeSerializer : UInt64Serializer {

        public UInt64SafeSerializer() : base(BsonType.Int64, new RepresentationConverter(true, false)) {

        }

        public UInt64SafeSerializer(BsonType representation) : base(representation, new RepresentationConverter(true, false)) {

        }
    }
}
