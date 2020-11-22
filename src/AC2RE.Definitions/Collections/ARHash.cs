using System;
using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class ARHash : Dictionary<uint, IPackage>, IPackage {

        public NativeType nativeType => NativeType.ARHASH;

        public Dictionary<K, V> to<K, V>() {
            return to<K, V>(v => (V)v);
        }

        public Dictionary<K, V> to<K, V>(Func<IPackage, V> valueConversion) {
            Dictionary<K, V> converted = new(Count);
            Converter<uint> keyConverter = Converters.getUInt(typeof(K));
            foreach ((var key, var value) in this) {
                converted[keyConverter.read<K>(key)] = valueConversion.Invoke(value);
            }
            return converted;
        }

        public static ARHash from<K, V>(Dictionary<K, V> source) where V : IPackage {
            return from(source, v => v);
        }

        public static ARHash from<K, V>(Dictionary<K, V> source, Func<V, IPackage> valueConversion) {
            if (source == null) {
                return null;
            }

            ARHash converted = new(source.Count);
            Converter<uint> keyConverter = Converters.getUInt(typeof(K));
            foreach ((var key, var value) in source) {
                converted[keyConverter.write(key)] = valueConversion.Invoke(value);
            }
            return converted;
        }

        private ARHash(int capacity) : base(capacity) {

        }

        public ARHash(AC2Reader data) {
            foreach ((var key, var value) in data.ReadDictionary(data.ReadUInt32, data.ReadPackageId)) {
                data.packageRegistry.addResolver(() => this[key] = data.packageRegistry.get<IPackage>(value));
            }
        }

        public void write(AC2Writer data) {
            data.Write(this, data.Write, data.WritePkg);
        }
    }
}
