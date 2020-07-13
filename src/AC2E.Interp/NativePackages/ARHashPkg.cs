using AC2E.Utils;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public class ARHashPkg<V> : IPackage where V : IPackage {

        public NativeType nativeType => NativeType.ARHASH;

        public Dictionary<uint, PkgRef<V>> contents;

        public ARHashPkg<T> to<T>() where T : IPackage {
            ARHashPkg<T> converted = new ARHashPkg<T>();
            if (contents != null) {
                converted.contents = new Dictionary<uint, PkgRef<T>>(contents.Count);
                foreach (var element in contents) {
                    converted.contents[element.Key] = new PkgRef<T>(element.Value.id);
                }
            }
            return converted;
        }

        public ARHashPkg() {

        }

        public ARHashPkg(BinaryReader data) {
            contents = data.ReadDictionary(data.ReadUInt32, () => data.ReadPkgRef<V>());
        }

        public void write(BinaryWriter data, List<PkgRef<IPackage>> references) {
            data.Write(contents, data.Write, v => data.Write(v, references));
        }

        public override string ToString() {
            return Util.objectToString(contents);
        }
    }
}
