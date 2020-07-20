using AC2E.Utils;
using System.Collections.Generic;

namespace AC2E.Def {

    public class ARHash<V> : IPackage, IDelegateToString where V : IPackage {

        public NativeType nativeType => NativeType.ARHASH;
        public object delegatedToStringObject => contents;

        public Dictionary<uint, V> contents;

        public ARHash<T> to<T>() where T : class, IPackage {
            ARHash<T> converted = new ARHash<T>();
            if (contents != null) {
                converted.contents = new Dictionary<uint, T>(contents.Count);
                foreach (var element in contents) {
                    converted.contents[element.Key] = element.Value as T;
                }
            }
            return converted;
        }

        public ARHash() {

        }

        public ARHash(AC2Reader data) {
            contents = new Dictionary<uint, V>();
            foreach (var element in data.ReadDictionary(data.ReadUInt32, data.ReadPackageId)) {
                data.packageRegistry.addResolver(() => contents[element.Key] = data.packageRegistry.get<V>(element.Value));
            }
        }

        public void write(AC2Writer data) {
            data.Write(contents, data.Write, data.WritePkg);
        }
    }
}
