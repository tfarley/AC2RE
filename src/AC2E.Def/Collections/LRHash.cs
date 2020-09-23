using System.Collections.Generic;

namespace AC2E.Def {

    public class LRHash<V> : Dictionary<ulong, V>, IPackage where V : IPackage {

        public NativeType nativeType => NativeType.LRHASH;

        public LRHash<T> to<T>() where T : class, IPackage {
            LRHash<T> converted = new LRHash<T>(Count);
            foreach (var element in this) {
                converted[element.Key] = element.Value as T;
            }
            return converted;
        }

        public LRHash() {

        }

        public LRHash(int capacity) : base(capacity) {

        }

        public LRHash(AC2Reader data) {
            foreach (var element in data.ReadDictionary(data.ReadUInt64, data.ReadPackageId)) {
                data.packageRegistry.addResolver(() => this[element.Key] = data.packageRegistry.get<V>(element.Value));
            }
        }

        public void write(AC2Writer data) {
            data.Write(this, data.Write, data.WritePkg);
        }
    }
}
