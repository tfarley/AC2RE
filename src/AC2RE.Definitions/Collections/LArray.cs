using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class LArray : List<ulong>, IPackage {

        public NativeType nativeType => NativeType.LArray;

        public List<T> to<T>() {
            List<T> converted = new(Count);
            Converter<ulong> elementConverter = Converters.getULong(typeof(T));
            foreach (var element in this) {
                converted.Add(elementConverter.read<T>(element));
            }
            return converted;
        }

        public static LArray from<T>(List<T> source) {
            if (source == null) {
                return null;
            }

            LArray converted = new(source.Count);
            Converter<ulong> elementConverter = Converters.getULong(typeof(T));
            foreach (var element in source) {
                converted.Add(elementConverter.write(element));
            }
            return converted;
        }

        private LArray(int capacity) : base(capacity) {

        }

        public LArray(AC2Reader data) {
            data.ReadList(this, data.ReadUInt64);
        }

        public void write(AC2Writer data) {
            data.Write(this, data.Write);
        }
    }
}
