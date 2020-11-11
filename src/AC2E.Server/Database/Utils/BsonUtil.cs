using MongoDB.Bson.Serialization;

namespace AC2E.Server.Database {

    internal static class BsonUtil {

        public static BsonClassMapSerializer<T> existingClassSerializer<T>() {
            return new(BsonClassMap.LookupClassMap(typeof(T)));
        }
    }
}
