using AC2E.Utils;
using System.Collections.Generic;

namespace AC2E.Def {

    public class LRHash<V> : IPackage, IDelegateToString where V : IPackage {

        public NativeType nativeType => NativeType.LRHASH;
        public object delegatedToStringObject => contents;

        public Dictionary<ulong, V> contents;

        public LRHash<T> to<T>() where T : class, IPackage {
            LRHash<T> converted = new LRHash<T>();
            if (contents != null) {
                converted.contents = new Dictionary<ulong, T>(contents.Count);
                foreach (var element in contents) {
                    converted.contents[element.Key] = element.Value as T;
                }
            }
            return converted;
        }

        public LRHash() {

        }

        public LRHash(AC2Reader data) {
            contents = new Dictionary<ulong, V>();
            foreach (var element in data.ReadDictionary(data.ReadUInt64, data.ReadPackageId)) {
                data.packageRegistry.addResolver(() => contents[element.Key] = data.packageRegistry.get<V>(element.Value));
            }
        }

        public void write(AC2Writer data) {
            data.Write(contents, data.Write, data.WritePkg);
        }
    }
}
