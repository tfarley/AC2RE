using System.Collections.Generic;

namespace AC2E.Def {

    public class AHashSet : HashSet<uint>, IPackage {

        public NativeType nativeType => NativeType.AHASHSET;

        public HashSet<T> to<T>() {
            HashSet<T> converted = new HashSet<T>(Count);
            Converter<uint> elementConverter = Converters.getUInt(typeof(T));
            foreach (var element in this) {
                converted.Add(elementConverter.read<T>(element));
            }
            return converted;
        }

        public static AHashSet from<T>(HashSet<T> source) {
            if (source == null) {
                return null;
            }

            AHashSet converted = new AHashSet(source.Count);
            Converter<uint> elementConverter = Converters.getUInt(typeof(T));
            foreach (var element in source) {
                converted.Add(elementConverter.write(element));
            }
            return converted;
        }

        private AHashSet(int capacity) : base(capacity) {

        }

        public AHashSet(AC2Reader data) {
            data.ReadSet(this, data.ReadUInt32);
        }

        public void write(AC2Writer data) {
            data.Write(this, data.Write);
        }
    }
}
