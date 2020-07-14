using AC2E.Utils;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public class ARHashPkg<V> : IPackage where V : IPackage {

        public NativeType nativeType => NativeType.ARHASH;

        public Dictionary<uint, V> contents;

        public ARHashPkg<T> to<T>() where T : class, IPackage {
            ARHashPkg<T> converted = new ARHashPkg<T>();
            if (contents != null) {
                converted.contents = new Dictionary<uint, T>(contents.Count);
                foreach (var element in contents) {
                    converted.contents[element.Key] = element.Value as T;
                }
            }
            return converted;
        }

        public ARHashPkg() {

        }

        public ARHashPkg(BinaryReader data, PackageRegistry registry) {
            contents = new Dictionary<uint, V>();
            foreach (var element in data.ReadDictionary(data.ReadUInt32, data.ReadPackageId)) {
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
