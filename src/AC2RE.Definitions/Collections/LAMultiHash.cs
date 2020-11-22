using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class LAMultiHash : Dictionary<ulong, List<uint>>, IPackage {

        public NativeType nativeType => NativeType.LAMULTIHASH;

        public Dictionary<K, List<V>> to<K, V>() {
            Dictionary<K, List<V>> converted = new(Count);
            Converter<ulong> keyConverter = Converters.getULong(typeof(K));
            Converter<uint> valueConverter = Converters.getUInt(typeof(V));
            foreach ((var key, var value) in this) {
                List<V> convertedValueList = new();
                foreach (var element in value) {
                    convertedValueList.Add(valueConverter.read<V>(element));
                }

                converted[keyConverter.read<K>(key)] = convertedValueList;
            }
            return converted;
        }

        public static LAMultiHash from<K, V>(Dictionary<K, List<V>> source) {
            if (source == null) {
                return null;
            }

            LAMultiHash converted = new(source.Count);
            Converter<ulong> keyConverter = Converters.getULong(typeof(K));
            Converter<uint> valueConverter = Converters.getUInt(typeof(V));
            foreach ((var key, var value) in source) {
                List<uint> convertedValueList = new();
                foreach (var element in value) {
                    convertedValueList.Add(valueConverter.write(element));
                }

                converted[keyConverter.write(key)] = convertedValueList;
            }
            return converted;
        }

        private LAMultiHash(int capacity) : base(capacity) {

        }

        public LAMultiHash(AC2Reader data) {
            data.ReadMultiDictionary(this, data.ReadUInt64, data.ReadUInt32);
        }

        public void write(AC2Writer data) {
            data.WriteMulti(this, data.Write, data.Write);
        }
    }
}
