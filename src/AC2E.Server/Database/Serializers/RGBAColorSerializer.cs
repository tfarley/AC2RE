using AC2E.Def;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace AC2E.Server.Database {

    internal class RGBAColorSerializer : StructSerializerBase<RGBAColor> {

        public override RGBAColor Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args) {
            context.Reader.ReadStartArray();
            RGBAColor value = new RGBAColor((float)context.Reader.ReadDouble(), (float)context.Reader.ReadDouble(), (float)context.Reader.ReadDouble(), (float)context.Reader.ReadDouble());
            context.Reader.ReadEndArray();
            return value;
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, RGBAColor value) {
            context.Writer.WriteStartArray();
            context.Writer.WriteDouble(value.r);
            context.Writer.WriteDouble(value.g);
            context.Writer.WriteDouble(value.b);
            context.Writer.WriteDouble(value.a);
            context.Writer.WriteEndArray();
        }
    }
}
