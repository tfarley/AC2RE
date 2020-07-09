using AC2E.Def;
using AC2E.Utils;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public class VectorPkg : IPackage {

        public NativeType nativeType => NativeType.VECTOR;
        public PackageType packageType => PackageType.UNDEF;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAPOBJECT);

        public PackageId id { get; set; }

        public Vector contents;

        public VectorPkg() {

        }

        public VectorPkg(BinaryReader data) {
            contents = data.ReadVector();
        }

        public void write(BinaryWriter data, List<PkgRef<IPackage>> references) {
            data.Write(contents);
        }

        public override string ToString() {
            return Util.objectToString(contents);
        }
    }
}
