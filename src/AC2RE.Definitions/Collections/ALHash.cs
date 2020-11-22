using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class ALHash : Dictionary<uint, ulong>, IPackage {

        public NativeType nativeType => NativeType.ALHASH;

        public Dictionary<K, V> to<K, V>() {
            Dictionary<K, V> converted = new(Count);
            Converter<uint> keyConverter = Converters.getUInt(typeof(K));
            Converter<ulong> valueConverter = Converters.getULong(typeof(V));
            foreach ((var key, var value) in this) {
                converted[keyConverter.read<K>(key)] = valueConverter.read<V>(value);
            }
            return converted;
        }

        public static ALHash from<K, V>(Dictionary<K, V> source) {
            if (source == null) {
                return null;
            }

            ALHash converted = new(source.Count);
            Converter<uint> keyConverter = Converters.getUInt(typeof(K));
            Converter<ulong> valueConverter = Converters.getULong(typeof(V));
            foreach ((var key, var value) in source) {
                converted[keyConverter.write(key)] = valueConverter.write(value);
            }
            return converted;
        }

        private ALHash(int capacity) : base(capacity) {

        }

        public ALHash(AC2Reader data) {
            data.ReadDictionary(this, data.ReadUInt32, data.ReadUInt64);
        }

        public void write(AC2Writer data) {
            data.Write(this, data.Write, data.Write);
        }
    }
}
