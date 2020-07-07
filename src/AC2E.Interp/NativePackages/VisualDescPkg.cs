using AC2E.Def;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public class VisualDescPkg : IPackage {

        public NativeType nativeType => NativeType.VISUALDESC;
        public PackageType packageType => PackageType.UNDEF;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAPOBJECT);

        public PackageId id { get; set; }

        public VisualDesc vDesc;

        public VisualDescPkg() {

        }

        public VisualDescPkg(BinaryReader data) {
            vDesc = new VisualDesc(data);
        }

        public void write(BinaryWriter data, List<IPackage> references) {
            vDesc.write(data);
        }
    }
}
