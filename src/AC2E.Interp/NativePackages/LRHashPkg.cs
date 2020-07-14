using AC2E.Utils;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public class LRHashPkg<V> : IPackage where V : IPackage {

        public NativeType nativeType => NativeType.LRHASH;

        public Dictionary<ulong, V> contents;

        public LRHashPkg<T> to<T>() where T : class, IPackage {
            LRHashPkg<T> converted = new LRHashPkg<T>();
            if (contents != null) {
                converted.contents = new Dictionary<ulong, T>(contents.Count);
                foreach (var element in contents) {
                    converted.contents[element.Key] = element.Value as T;
                }
            }
            return converted;
        }

        public LRHashPkg() {

        }

        public LRHashPkg(BinaryReader data, PackageRegistry registry) {
            contents = new Dictionary<ulong, V>();
            foreach (var element in data.ReadDictionary(data.ReadUInt64, data.ReadPackageId)) {
                registry.addResolver(() => contents[element.Key] = registry.get<V>(element.Value));
            }
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(contents, data.Write, v => data.Write(v, registry));
        }

        public override string ToString() {
            return Util.objectToString(contents);
        }
    }
}
