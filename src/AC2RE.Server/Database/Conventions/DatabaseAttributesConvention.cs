using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using System.Reflection;

namespace AC2RE.Server.Database {

    internal class DatabaseAttributesConvention : IClassMapConvention {

        public string Name => "Database attributes";

        public void Apply(BsonClassMap classMap) {
            MemberInfo[] idMembers = classMap.ClassType.FindMembers(MemberTypes.All, (BindingFlags)(-1), (m, f) => m.GetCustomAttribute<DbIdAttribute>() != null, null);
            foreach (MemberInfo idMember in idMembers) {
                classMap.MapIdMember(idMember);
            }

            MemberInfo[] constructorMembers = classMap.ClassType.FindMembers(MemberTypes.All, (BindingFlags)(-1), (m, f) => m.GetCustomAttribute<DbConstructorAttribute>() != null, null);
            foreach (MemberInfo constructorMember in constructorMembers) {
                ConstructorInfo constructorInfo = (ConstructorInfo)constructorMember;
                ParameterInfo[] parameterInfos = constructorInfo.GetParameters();
                string[] parameterNames = new string[parameterInfos.Length];
                for (int i = 0; i < parameterInfos.Length; i++) {
                    parameterNames[i] = parameterInfos[i].Name!;
                }
                classMap.MapConstructor(constructorInfo, parameterNames);
            }

            MemberInfo[] persistMembers = classMap.ClassType.FindMembers(MemberTypes.All, (BindingFlags)(-1), (m, f) => m.GetCustomAttribute<DbPersistAttribute>() != null, null);
            foreach (MemberInfo persistMember in persistMembers) {
                classMap.MapMember(persistMember);
            }
        }
    }
}
