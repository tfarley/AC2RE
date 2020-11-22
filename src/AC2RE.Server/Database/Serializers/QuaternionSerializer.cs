using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System.Numerics;

namespace AC2RE.Server.Database {

    internal class QuaternionSerializer : StructSerializerBase<Quaternion> {

        public override Quaternion Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args) {
            context.Reader.ReadStartArray();
            Quaternion value = new((float)context.Reader.ReadDouble(), (float)context.Reader.ReadDouble(), (float)context.Reader.ReadDouble(), (float)context.Reader.ReadDouble());
            context.Reader.ReadEndArray();
            return value;
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Quaternion value) {
            context.Writer.WriteStartArray();
            context.Writer.WriteDouble(value.X);
            context.Writer.WriteDouble(value.Y);
            context.Writer.WriteDouble(value.Z);
            context.Writer.WriteDouble(value.W);
            context.Writer.WriteEndArray();
        }
    }
}
