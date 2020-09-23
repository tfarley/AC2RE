using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;

namespace AC2E.Server.Database {

    internal class UInt64IdSerializer<T> : StructSerializerBase<T>, IRepresentationConfigurable<UInt64IdSerializer<T>>, IRepresentationConfigurable where T : struct {

        private readonly Func<ulong, T> converter;
        private readonly Func<T, ulong> extractor;
        private readonly UInt64SafeSerializer uInt64Serializer;

        public BsonType Representation => uInt64Serializer.Representation;

        public UInt64IdSerializer(Func<ulong, T> converter, Func<T, ulong> extractor) {
            this.converter = converter;
            this.extractor = extractor;
            uInt64Serializer = new UInt64SafeSerializer();
        }

        public UInt64IdSerializer(Func<ulong, T> converter, Func<T, ulong> extractor, BsonType representation) {
            this.converter = converter;
            this.extractor = extractor;
            uInt64Serializer = new UInt64SafeSerializer(representation);
        }

        public override T Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args) {
            return converter.Invoke(uInt64Serializer.Deserialize(context, args));
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, T value) {
            uInt64Serializer.Serialize(context, args, extractor.Invoke(value));
        }

        public UInt64IdSerializer<T> WithRepresentation(BsonType representation) {
            return representation == Representation ? this : new UInt64IdSerializer<T>(converter, extractor, representation);
        }

        IBsonSerializer IRepresentationConfigurable.WithRepresentation(BsonType representation) {
            return WithRepresentation(representation);
        }
    }
}
