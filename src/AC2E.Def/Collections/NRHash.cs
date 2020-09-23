using System.Collections.Generic;

namespace AC2E.Def {

    public class NRHash<K, V> : Dictionary<K, V>, IPackage where K : IPackage where V : IPackage {

        public NativeType nativeType => NativeType.NRHASH;

        public NRHash<T, U> to<T, U>() where T : class, IPackage where U : class, IPackage {
            NRHash<T, U> converted = new NRHash<T, U>(Count);
            foreach (var element in this) {
                converted[element.Key as T] = element.Value as U;
            }
            return converted;
        }

        public NRHash() {

        }

        public NRHash(int capacity) : base(capacity) {

        }

        public NRHash(AC2Reader data) {
            foreach (var element in data.ReadDictionary(() => new ReferenceId(data).id, data.ReadPackageId)) {
                data.packageRegistry.addResolver(() => this[data.packageRegistry.get<K>(element.Key)] = data.packageRegistry.get<V>(element.Value));
            }
        }

        public void write(AC2Writer data) {
            data.Write(this, data.WritePkg, data.WritePkg);
        }
    }
}
