using AC2E.Def;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;

namespace AC2E.Server.Database {

    internal class InstanceIdSerializer : StructSerializerBase<InstanceId>, IRepresentationConfigurable<InstanceIdSerializer> {

        public BsonType Representation { get; }

        public InstanceIdSerializer() : this(BsonType.Int64) {

        }

        public InstanceIdSerializer(BsonType representation) {
            switch (representation) {
                case BsonType.Int64:
                case BsonType.String:
                    break;

                default:
                    throw new ArgumentException($"{representation} is not a valid representation for a InstanceIdSerializer.");
            }

            Representation = representation;
        }

        public override InstanceId Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args) {
            var bsonType = context.Reader.GetCurrentBsonType();
            switch (bsonType) {
                case BsonType.Int64:
                    return new InstanceId((ulong)context.Reader.ReadInt64());

                case BsonType.String:
                    return new InstanceId(ulong.Parse(context.Reader.ReadString()));

                default:
                    throw CreateCannotDeserializeFromBsonTypeException(bsonType);
            }
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, InstanceId value) {
            switch (Representation) {
                case BsonType.Int64:
                    context.Writer.WriteInt64((long)value.id);
                    break;

                case BsonType.String:
                    context.Writer.WriteString(value.id.ToString());
                    break;

                default:
                    throw new BsonSerializationException($"'{Representation}' is not a valid InstanceId representation.");
            }
        }

        public InstanceIdSerializer WithRepresentation(BsonType representation) {
            return representation == Representation ? this : new InstanceIdSerializer(representation);
        }

        IBsonSerializer IRepresentationConfigurable.WithRepresentation(BsonType representation) {
            return WithRepresentation(representation);
        }
    }
}
