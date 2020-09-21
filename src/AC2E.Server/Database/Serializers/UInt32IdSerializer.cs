using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;

namespace AC2E.Server.Database {

    internal class UInt32IdSerializer<T> : StructSerializerBase<T>, IRepresentationConfigurable<UInt32IdSerializer<T>>, IRepresentationConfigurable where T : struct {

        private readonly Func<uint, T> converter;
        private readonly Func<T, uint> extractor;

        public BsonType Representation { get; }

        public UInt32IdSerializer(Func<uint, T> converter, Func<T, uint> extractor) : this(converter, extractor, BsonType.Int32) {

        }

        public UInt32IdSerializer(Func<uint, T> converter, Func<T, uint> extractor, BsonType representation) {
            this.converter = converter;
            this.extractor = extractor;

            switch (representation) {
                case BsonType.Int32:
                case BsonType.String:
                    break;

                default:
                    throw new ArgumentException($"{representation} is not a valid representation for an UInt32IdSerializer.");
            }

            Representation = representation;
        }

        public override T Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args) {
            var bsonType = context.Reader.GetCurrentBsonType();
            switch (bsonType) {
                case BsonType.Int32:
                    return converter.Invoke((uint)context.Reader.ReadInt32());

                case BsonType.String:
                    return converter.Invoke(uint.Parse(context.Reader.ReadString()));

                default:
                    throw CreateCannotDeserializeFromBsonTypeException(bsonType);
            }
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, T value) {
            switch (Representation) {
                case BsonType.Int32:
                    context.Writer.WriteInt32((int)extractor.Invoke(value));
                    break;

                case BsonType.String:
                    context.Writer.WriteString(extractor.Invoke(value).ToString());
                    break;

                default:
                    throw new BsonSerializationException($"'{Representation}' is not a valid UInt32Id representation.");
            }
        }

        public UInt32IdSerializer<T> WithRepresentation(BsonType representation) {
            return representation == Representation ? this : new UInt32IdSerializer<T>(converter, extractor, representation);
        }

        IBsonSerializer IRepresentationConfigurable.WithRepresentation(BsonType representation) {
            return WithRepresentation(representation);
        }
    }
}
