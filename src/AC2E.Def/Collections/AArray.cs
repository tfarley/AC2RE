using System.Collections.Generic;

namespace AC2E.Def {

    public class AArray : List<uint>, IPackage {

        public NativeType nativeType => NativeType.AARRAY;

        public List<T> to<T>() {
            List<T> converted = new(Count);
            Converter<uint> elementConverter = Converters.getUInt(typeof(T));
            foreach (var element in this) {
                converted.Add(elementConverter.read<T>(element));
            }
            return converted;
        }

        public static AArray from<T>(List<T> source) {
            if (source == null) {
                return null;
            }

            AArray converted = new(source.Count);
            Converter<uint> elementConverter = Converters.getUInt(typeof(T));
            foreach (var element in source) {
                converted.Add(elementConverter.write(element));
            }
            return converted;
        }

        private AArray(int capacity) : base(capacity) {

        }

        public AArray(AC2Reader data) {
            data.ReadList(this, data.ReadUInt32);
        }

        public void write(AC2Writer data) {
            data.Write(this, data.Write);
        }
    }
}
