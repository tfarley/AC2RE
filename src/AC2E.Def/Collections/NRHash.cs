using AC2E.Utils;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Def {

    public class NRHash<K, V> : IPackage, IDelegateToString where K : IPackage where V : IPackage {

        public NativeType nativeType => NativeType.NRHASH;
        public object delegatedToStringObject => contents;

        public Dictionary<K, V> contents;

        public NRHash<T, U> to<T, U>() where T : class, IPackage where U : class, IPackage {
            NRHash<T, U> converted = new NRHash<T, U>();
            if (contents != null) {
                converted.contents = new Dictionary<T, U>(contents.Count);
                foreach (var element in contents) {
                    converted.contents[element.Key as T] = element.Value as U;
                }
            }
            return converted;
        }

        public NRHash() {

        }

        public NRHash(BinaryReader data, PackageRegistry registry) {
            contents = new Dictionary<K, V>();
            foreach (var element in data.ReadDictionary(() => new ReferenceId(data).id, data.ReadPackageId)) {
                registry.addResolver(() => contents[registry.get<K>(element.Key)] = registry.get<V>(element.Value));
            }
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(contents, k => data.Write(k, registry), v => data.Write(v, registry));
        }
    }
}
