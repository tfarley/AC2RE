using System;
using System.Collections.Generic;

namespace AC2E.Def {

    public struct Converter<T> {

        private readonly Func<T, object> readFunc;
        private readonly Func<object, T> writeFunc;

        public Converter(Func<T, object> readFunc, Func<object, T> writeFunc) {
            this.readFunc = readFunc;
            this.writeFunc = writeFunc;
        }

        public U read<U>(T value) {
            return (U)readFunc.Invoke(value);
        }

        public T write(object value) {
            return writeFunc.Invoke(value);
        }
    }

    public static class Converters {

        private static readonly Dictionary<Type, Converter<ulong>> ULONG = new() {
            { typeof(InstanceId), new Converter<ulong>(v => new InstanceId(v), v => ((InstanceId)v).id) },
        };

        public static Converter<ulong> getULong(Type type) {
            foreach ((var convertedType, var converter) in ULONG) {
                if (convertedType.IsAssignableFrom(type)) {
                    return converter;
                }
            }

            return new Converter<ulong>(v => v, v => (ulong)v);
        }

        private static readonly Dictionary<Type, Converter<uint>> UINT = new() {
            { typeof(bool), new Converter<uint>(v => v != 0, v => (bool)v ? 1u : 0u) },
            { typeof(DataId), new Converter<uint>(v => new DataId(v), v => ((DataId)v).id) },
        };

        public static Converter<uint> getUInt(Type type) {
            foreach ((var convertedType, var converter) in UINT) {
                if (convertedType.IsAssignableFrom(type)) {
                    return converter;
                }
            }

            return new Converter<uint>(v => v, v => (uint)v);
        }
    }
}
