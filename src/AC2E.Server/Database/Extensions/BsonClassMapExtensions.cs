using MongoDB.Bson.Serialization;
using System.Reflection;

namespace AC2E.Server.Database {

    internal static class BsonClassMapExtensions {

        public static void autoMapCustom(this BsonClassMap classMap) {
            classMap.AutoMap();
            MemberInfo[] ignoreMembers = classMap.ClassType.FindMembers(MemberTypes.All, (BindingFlags)(-1), (m, f) => m.GetCustomAttribute<DatabaseIgnoreAttribute>() != null, null);
            foreach (MemberInfo ignoreMember in ignoreMembers) {
                classMap.UnmapMember(ignoreMember);
            }
        }
    }
}
