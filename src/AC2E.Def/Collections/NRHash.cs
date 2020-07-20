using AC2E.Utils;
using System.Collections.Generic;

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

        public NRHash(AC2Reader data) {
            contents = new Dictionary<K, V>();
            foreach (var element in data.ReadDictionary(() => new ReferenceId(data).id, data.ReadPackageId)) {
                data.packageRegistry.addResolver(() => contents[data.packageRegistry.get<K>(element.Key)] = data.packageRegistry.get<V>(element.Value));
            }
        }

        public void write(AC2Writer data) {
            data.Write(contents, data.WritePkg, data.WritePkg);
        }
    }
}
