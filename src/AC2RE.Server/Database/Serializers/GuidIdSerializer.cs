using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;

namespace AC2RE.Server.Database {

    internal class GuidIdSerializer<T> : StructSerializerBase<T>, IRepresentationConfigurable<GuidIdSerializer<T>>, IRepresentationConfigurable where T : struct {

        private readonly Func<Guid, T> converter;
        private readonly Func<T, Guid> extractor;
        private readonly GuidSerializer guidSerializer;

        public BsonType Representation => guidSerializer.Representation;

        public GuidIdSerializer(Func<Guid, T> converter, Func<T, Guid> extractor) {
            this.converter = converter;
            this.extractor = extractor;
            guidSerializer = new();
        }

        public GuidIdSerializer(Func<Guid, T> converter, Func<T, Guid> extractor, BsonType representation) {
            this.converter = converter;
            this.extractor = extractor;
            guidSerializer = new(representation);
        }

        public override T Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args) {
            return converter.Invoke(guidSerializer.Deserialize(context, args));
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, T value) {
            guidSerializer.Serialize(context, args, extractor.Invoke(value));
        }

        public GuidIdSerializer<T> WithRepresentation(BsonType representation) {
            return representation == Representation ? this : new(converter, extractor, representation);
        }

        IBsonSerializer IRepresentationConfigurable.WithRepresentation(BsonType representation) {
            return WithRepresentation(representation);
        }
    }
}
