using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class AList : List<uint>, IPackage {

        public NativeType nativeType => NativeType.AList;

        public List<T> to<T>() {
            List<T> converted = new(Count);
            Converter<uint> elementConverter = Converters.getUInt(typeof(T));
            foreach (var element in this) {
                converted.Add(elementConverter.read<T>(element));
            }
            return converted;
        }

        public static AList from<T>(List<T> source) {
            if (source == null) {
                return null;
            }

            AList converted = new(source.Count);
            Converter<uint> elementConverter = Converters.getUInt(typeof(T));
            foreach (var element in source) {
                converted.Add(elementConverter.write(element));
            }
            return converted;
        }

        private AList(int capacity) : base(capacity) {

        }

        public AList(AC2Reader data) {
            data.ReadList(this, data.ReadUInt32);
        }

        public void write(AC2Writer data) {
            data.Write(this, data.Write);
        }
    }
}
