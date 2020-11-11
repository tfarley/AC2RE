using System.Collections.Generic;

namespace AC2E.Def {

    public class LAHash : Dictionary<ulong, uint>, IPackage {

        public NativeType nativeType => NativeType.LAHASH;

        public Dictionary<K, V> to<K, V>() {
            Dictionary<K, V> converted = new(Count);
            Converter<ulong> keyConverter = Converters.getULong(typeof(K));
            Converter<uint> valueConverter = Converters.getUInt(typeof(V));
            foreach ((var key, var value) in this) {
                converted[keyConverter.read<K>(key)] = valueConverter.read<V>(value);
            }
            return converted;
        }

        public static LAHash from<K, V>(Dictionary<K, V> source) {
            if (source == null) {
                return null;
            }

            LAHash converted = new(source.Count);
            Converter<ulong> keyConverter = Converters.getULong(typeof(K));
            Converter<uint> valueConverter = Converters.getUInt(typeof(V));
            foreach ((var key, var value) in source) {
                converted[keyConverter.write(key)] = valueConverter.write(value);
            }
            return converted;
        }

        private LAHash(int capacity) : base(capacity) {

        }

        public LAHash(AC2Reader data) {
            data.ReadDictionary(this, data.ReadUInt64, data.ReadUInt32);
        }

        public void write(AC2Writer data) {
            data.Write(this, data.Write, data.Write);
        }
    }
}
