using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;

namespace AC2E.Server.Database {

    internal class UInt32IdSerializer<T> : StructSerializerBase<T>, IRepresentationConfigurable<UInt32IdSerializer<T>>, IRepresentationConfigurable where T : struct {

        private readonly Func<uint, T> converter;
        private readonly Func<T, uint> extractor;
        private readonly UInt32SafeSerializer uInt32Serializer;

        public BsonType Representation => uInt32Serializer.Representation;

        public UInt32IdSerializer(Func<uint, T> converter, Func<T, uint> extractor) {
            this.converter = converter;
            this.extractor = extractor;
            uInt32Serializer = new UInt32SafeSerializer();
        }

        public UInt32IdSerializer(Func<uint, T> converter, Func<T, uint> extractor, BsonType representation) {
            this.converter = converter;
            this.extractor = extractor;
            uInt32Serializer = new UInt32SafeSerializer(representation);
        }

        public override T Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args) {
            return converter.Invoke(uInt32Serializer.Deserialize(context, args));
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, T value) {
            uInt32Serializer.Serialize(context, args, extractor.Invoke(value));
        }

        public UInt32IdSerializer<T> WithRepresentation(BsonType representation) {
            return representation == Representation ? this : new UInt32IdSerializer<T>(converter, extractor, representation);
        }

        IBsonSerializer IRepresentationConfigurable.WithRepresentation(BsonType representation) {
            return WithRepresentation(representation);
        }
    }
}
