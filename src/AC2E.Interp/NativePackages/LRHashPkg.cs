using AC2E.Utils;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public class LRHashPkg<V> : IPackage where V : IPackage {

        public NativeType nativeType => NativeType.LRHASH;

        public Dictionary<ulong, PkgRef<V>> contents;

        public LRHashPkg<T> to<T>() where T : IPackage {
            LRHashPkg<T> converted = new LRHashPkg<T>();
            if (contents != null) {
                converted.contents = new Dictionary<ulong, PkgRef<T>>(contents.Count);
                foreach (var element in contents) {
                    converted.contents[element.Key] = new PkgRef<T>(element.Value.id);
                }
            }
            return converted;
        }

        public LRHashPkg() {

        }

        public LRHashPkg(BinaryReader data) {
            contents = data.ReadDictionary(data.ReadUInt64, () => data.ReadPkgRef<V>());
        }

        public void write(BinaryWriter data, List<PkgRef<IPackage>> references) {
            data.Write(contents, data.Write, v => data.Write(v, references));
        }

        public override string ToString() {
            return Util.objectToString(contents);
        }
    }
}
