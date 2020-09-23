using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using System.Reflection;

namespace AC2E.Server.Database {

    internal class DatabaseIgnoreConvention : IClassMapConvention {

        public string Name => "Database ignore annotation";

        public void Apply(BsonClassMap classMap) {
            MemberInfo[] ignoreMembers = classMap.ClassType.FindMembers(MemberTypes.All, (BindingFlags)(-1), (m, f) => m.GetCustomAttribute<DatabaseIgnoreAttribute>() != null, null);
            foreach (MemberInfo ignoreMember in ignoreMembers) {
                classMap.UnmapMember(ignoreMember);
            }
        }
    }
}
