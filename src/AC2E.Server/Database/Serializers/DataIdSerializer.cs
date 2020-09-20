using AC2E.Def;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;

namespace AC2E.Server.Database {

    internal class DataIdSerializer : StructSerializerBase<DataId>, IRepresentationConfigurable<DataIdSerializer> {

        public BsonType Representation { get; }

        public DataIdSerializer() : this(BsonType.Int32) {

        }

        public DataIdSerializer(BsonType representation) {
            switch (representation) {
                case BsonType.Int32:
                case BsonType.String:
                    break;

                default:
                    throw new ArgumentException($"{representation} is not a valid representation for a DataIdSerializer.");
            }

            Representation = representation;
        }

        public override DataId Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args) {
            var bsonType = context.Reader.GetCurrentBsonType();
            switch (bsonType) {
                case BsonType.Int32:
                    return new DataId((uint)context.Reader.ReadInt32());

                case BsonType.String:
                    return new DataId(uint.Parse(context.Reader.ReadString()));

                default:
                    throw CreateCannotDeserializeFromBsonTypeException(bsonType);
            }
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, DataId value) {
            switch (Representation) {
                case BsonType.Int32:
                    context.Writer.WriteInt32((int)value.id);
                    break;

                case BsonType.String:
                    context.Writer.WriteString(value.id.ToString());
                    break;

                default:
                    throw new BsonSerializationException($"'{Representation}' is not a valid DataId representation.");
            }
        }

        public DataIdSerializer WithRepresentation(BsonType representation) {
            return representation == Representation ? this : new DataIdSerializer(representation);
        }

        IBsonSerializer IRepresentationConfigurable.WithRepresentation(BsonType representation) {
            return WithRepresentation(representation);
        }
    }
}
