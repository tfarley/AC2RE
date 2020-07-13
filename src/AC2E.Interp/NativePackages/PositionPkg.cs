using AC2E.Def;
using AC2E.Utils;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public class PositionPkg : IPackage {

        public NativeType nativeType => NativeType.POSITION;

        public Position contents;

        public PositionPkg() {

        }

        public PositionPkg(BinaryReader data) {
            contents = new Position(data);
        }

        public void write(BinaryWriter data, List<PkgRef<IPackage>> references) {
            contents.write(data);
        }

        public override string ToString() {
            return Util.objectToString(contents);
        }
    }
}
