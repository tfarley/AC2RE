using MongoDB.Bson.Serialization;
using System;

namespace AC2RE.Server.Database {

    internal class SerializationProvider : IBsonSerializationProvider {

        public IBsonSerializer? GetSerializer(Type type) {
            if (type == typeof(uint) || (type.IsEnum && Enum.GetUnderlyingType(type) == typeof(uint))) {
                return new UInt32SafeSerializer();
            } else if (type == typeof(ulong) || (type.IsEnum && Enum.GetUnderlyingType(type) == typeof(ulong))) {
                return new UInt64SafeSerializer();
            }
            return null;
        }
    }
}
