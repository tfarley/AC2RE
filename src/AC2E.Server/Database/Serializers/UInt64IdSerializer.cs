using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;

namespace AC2E.Server.Database {

    internal class UInt64IdSerializer<T> : StructSerializerBase<T>, IRepresentationConfigurable<UInt64IdSerializer<T>>, IRepresentationConfigurable where T : struct {

        private readonly Func<ulong, T> converter;
        private readonly Func<T, ulong> extractor;

        public BsonType Representation { get; }

        public UInt64IdSerializer(Func<ulong, T> converter, Func<T, ulong> extractor) : this(converter, extractor, BsonType.Int64) {

        }

        public UInt64IdSerializer(Func<ulong, T> converter, Func<T, ulong> extractor, BsonType representation) {
            this.converter = converter;
            this.extractor = extractor;

            switch (representation) {
                case BsonType.Int64:
                case BsonType.String:
                    break;

                default:
                    throw new ArgumentException($"{representation} is not a valid representation for an UInt64IdSerializer.");
            }

            Representation = representation;
        }

        public override T Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args) {
            var bsonType = context.Reader.GetCurrentBsonType();
            switch (bsonType) {
                case BsonType.Int64:
                    return converter.Invoke((ulong)context.Reader.ReadInt64());

                case BsonType.String:
                    return converter.Invoke(ulong.Parse(context.Reader.ReadString()));

                default:
                    throw CreateCannotDeserializeFromBsonTypeException(bsonType);
            }
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, T value) {
            switch (Representation) {
                case BsonType.Int64:
                    context.Writer.WriteInt64((long)extractor.Invoke(value));
                    break;

                case BsonType.String:
                    context.Writer.WriteString(extractor.Invoke(value).ToString());
                    break;

                default:
                    throw new BsonSerializationException($"'{Representation}' is not a valid UInt64Id representation.");
            }
        }

        public UInt64IdSerializer<T> WithRepresentation(BsonType representation) {
            return representation == Representation ? this : new UInt64IdSerializer<T>(converter, extractor, representation);
        }

        IBsonSerializer IRepresentationConfigurable.WithRepresentation(BsonType representation) {
            return WithRepresentation(representation);
        }
    }
}
