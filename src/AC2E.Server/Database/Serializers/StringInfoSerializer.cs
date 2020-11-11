using AC2E.Def;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace AC2E.Server.Database {

    internal class StringInfoSerializer : ClassSerializerBase<StringInfo> {

        private readonly BsonClassMapSerializer<StringInfo> classMapSerializer;

        public StringInfoSerializer() {
            classMapSerializer = new(new BsonClassMap<StringInfo>(c => {
                c.MapField(r => r.tableDid);
                c.MapField(r => r.stringId).SetSerializer(new UInt32Serializer(BsonType.Int32, new(true, false)));
            }).Freeze());
        }

        public override StringInfo Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args) {
            switch (context.Reader.GetCurrentBsonType()) {
                case BsonType.Null:
                    context.Reader.ReadNull();
                    return null!;

                case BsonType.String:
                    return new(context.Reader.ReadString());

                default:
                    return classMapSerializer.Deserialize(context, args);
            }
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, StringInfo value) {
            if (value == null) {
                context.Writer.WriteNull();
            } else if (value.literalValue != null) {
                context.Writer.WriteString(value.literalValue);
            } else {
                classMapSerializer.Serialize(context, args, value);
            }
        }
    }
}
