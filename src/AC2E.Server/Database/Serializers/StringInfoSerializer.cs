using AC2E.Def;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;

namespace AC2E.Server.Database {

    internal class StringInfoSerializer : ClassSerializerBase<StringInfo> {

        private BsonClassMapSerializer<StringInfo> classMapSerializer;

        public StringInfoSerializer() {
            classMapSerializer = new BsonClassMapSerializer<StringInfo>(new BsonClassMap<StringInfo>(c => {
                c.MapField(r => r.tableDid);
                c.MapField(r => r.stringId).SetSerializer(new UInt32Serializer(BsonType.Int32, new RepresentationConverter(true, false)));
            }).Freeze());
        }

        public override StringInfo Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args) {
            var bsonType = context.Reader.GetCurrentBsonType();
            switch (bsonType) {
                case BsonType.Null:
                    context.Reader.ReadNull();
                    return null!;

                case BsonType.String:
                    return new StringInfo(context.Reader.ReadString());

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
