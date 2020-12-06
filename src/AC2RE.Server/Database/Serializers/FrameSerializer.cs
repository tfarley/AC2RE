using AC2RE.Definitions;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System.IO;

namespace AC2RE.Server.Database {

    internal class FrameSerializer : StructSerializerBase<Frame> {

        private readonly Vector3Serializer vector3Serializer = new();
        private readonly QuaternionSerializer quaternionSerializer = new();

        public override Frame Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args) {
            Frame value = new();

            context.Reader.ReadStartDocument();
            while (context.Reader.ReadBsonType() != BsonType.EndOfDocument) {
                readField(ref value, context.Reader.ReadName(), context, args);
            }
            context.Reader.ReadEndDocument();
            return value;
        }

        private void readField(ref Frame value, string fieldName, BsonDeserializationContext context, BsonDeserializationArgs args) {
            switch (fieldName) {
                case "pos":
                    value.pos = vector3Serializer.Deserialize(context, args);
                    break;
                case "rot":
                    value.rot = quaternionSerializer.Deserialize(context, args);
                    break;
                default:
                    throw new InvalidDataException(fieldName);
            }
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Frame value) {
            context.Writer.WriteStartDocument();
            context.Writer.WriteName("pos");
            vector3Serializer.Serialize(context, args, value.pos);
            context.Writer.WriteName("rot");
            quaternionSerializer.Serialize(context, args, value.rot);
            context.Writer.WriteEndDocument();
        }
    }
}
