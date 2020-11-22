using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class AAHash : Dictionary<uint, uint>, IPackage {

        public NativeType nativeType => NativeType.AAHASH;

        public Dictionary<K, V> to<K, V>() {
            Dictionary<K, V> converted = new(Count);
            Converter<uint> keyConverter = Converters.getUInt(typeof(K));
            Converter<uint> valueConverter = Converters.getUInt(typeof(V));
            foreach ((var key, var value) in this) {
                converted[keyConverter.read<K>(key)] = valueConverter.read<V>(value);
            }
            return converted;
        }

        public static AAHash from<K, V>(Dictionary<K, V> source) {
            if (source == null) {
                return null;
            }

            AAHash converted = new(source.Count);
            Converter<uint> keyConverter = Converters.getUInt(typeof(K));
            Converter<uint> valueConverter = Converters.getUInt(typeof(V));
            foreach ((var key, var value) in source) {
                converted[keyConverter.write(key)] = valueConverter.write(value);
            }
            return converted;
        }

        private AAHash(int capacity) : base(capacity) {

        }

        public AAHash(AC2Reader data) {
            data.ReadDictionary(this, data.ReadUInt32, data.ReadUInt32);
        }

        public void write(AC2Writer data) {
            data.Write(this, data.Write, data.Write);
        }
    }
}
