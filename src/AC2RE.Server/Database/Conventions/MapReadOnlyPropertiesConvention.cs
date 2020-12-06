using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using System.Reflection;

namespace AC2RE.Server.Database {

    internal class MapReadOnlyPropertiesConvention : IClassMapConvention {

        public string Name => "Map read only properties";

        public void Apply(BsonClassMap classMap) {
            MemberInfo[] addedMembers = classMap.ClassType.FindMembers(MemberTypes.Property, (BindingFlags)(-1), (m, f) => m is PropertyInfo propertyInfo && propertyInfo.CanRead && !propertyInfo.CanWrite && propertyInfo.GetIndexParameters().Length == 0, null);
            foreach (MemberInfo addedMember in addedMembers) {
                classMap.MapMember(addedMember);
            }
        }
    }
}
