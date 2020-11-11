using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System.Numerics;

namespace AC2E.Server.Database {

    internal class Vector3Serializer : StructSerializerBase<Vector3> {

        public override Vector3 Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args) {
            context.Reader.ReadStartArray();
            Vector3 value = new((float)context.Reader.ReadDouble(), (float)context.Reader.ReadDouble(), (float)context.Reader.ReadDouble());
            context.Reader.ReadEndArray();
            return value;
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Vector3 value) {
            context.Writer.WriteStartArray();
            context.Writer.WriteDouble(value.X);
            context.Writer.WriteDouble(value.Y);
            context.Writer.WriteDouble(value.Z);
            context.Writer.WriteEndArray();
        }
    }
}
