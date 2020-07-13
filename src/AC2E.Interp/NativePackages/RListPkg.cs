using AC2E.Utils;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public class RListPkg<T> : IPackage where T : IPackage {

        public NativeType nativeType => NativeType.RLIST;

        public List<PkgRef<T>> contents;

        public RListPkg<T> to<T>() where T : IPackage {
            RListPkg<T> converted = new RListPkg<T>();
            if (contents != null) {
                converted.contents = new List<PkgRef<T>>(contents.Count);
                foreach (var element in contents) {
                    converted.contents.Add(new PkgRef<T>(element.id));
                }
            }
            return converted;
        }

        public RListPkg() {

        }

        public RListPkg(BinaryReader data) {
            contents = data.ReadList(() => data.ReadPkgRef<T>());
        }

        public void write(BinaryWriter data, List<PkgRef<IPackage>> references) {
            data.Write(contents, v => data.Write(v, references));
        }

        public override string ToString() {
            return Util.objectToString(contents);
        }
    }
}
