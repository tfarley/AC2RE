using System.Collections.Generic;

namespace AC2E.Def {

    public class ARHash<V> : Dictionary<uint, V>, IPackage where V : IPackage {

        public NativeType nativeType => NativeType.ARHASH;

        public ARHash<T> to<T>() where T : class, IPackage {
            ARHash<T> converted = new ARHash<T>(Count);
            foreach (var element in this) {
                converted[element.Key] = element.Value as T;
            }
            return converted;
        }

        public ARHash() {

        }

        public ARHash(int capacity) : base(capacity) {

        }

        public ARHash(AC2Reader data) {
            foreach (var element in data.ReadDictionary(data.ReadUInt32, data.ReadPackageId)) {
                data.packageRegistry.addResolver(() => this[element.Key] = data.packageRegistry.get<V>(element.Value));
            }
        }

        public void write(AC2Writer data) {
            data.Write(this, data.Write, data.WritePkg);
        }
    }
}
