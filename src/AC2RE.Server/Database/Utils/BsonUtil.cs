using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;

namespace AC2RE.Server.Database {

    internal static class BsonUtil {

        public static BsonClassMapSerializer<T> existingClassSerializer<T>() {
            return new(BsonClassMap.LookupClassMap(typeof(T)));
        }

        public static void applyGlobalConventions(BsonClassMap classMap) {
            new ConventionRunner(MongoDatabase.GLOBAL_CONVENTIONS).Apply(classMap);
        }
    }
}
