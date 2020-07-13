using AC2E.Def;
using AC2E.Utils;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public class VisualDescPkg : IPackage {

        public NativeType nativeType => NativeType.VISUALDESC;

        public VisualDesc contents;

        public VisualDescPkg() {

        }

        public VisualDescPkg(BinaryReader data) {
            contents = new VisualDesc(data);
        }

        public void write(BinaryWriter data, List<PkgRef<IPackage>> references) {
            contents.write(data);
        }

        public override string ToString() {
            return Util.objectToString(contents);
        }
    }
}
