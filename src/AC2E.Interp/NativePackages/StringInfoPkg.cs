using AC2E.Def;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public class StringInfoPkg : IPackage {

        public NativeType nativeType => NativeType.STRINGINFO;
        public PackageType packageType => PackageType.UNDEF;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAPOBJECT);

        public PackageId id { get; set; }

        public StringInfo stringInfo;

        public StringInfoPkg() {

        }

        public StringInfoPkg(BinaryReader data) {
            stringInfo = new StringInfo(data);
        }

        public void write(BinaryWriter data, List<IPackage> references) {
            stringInfo.write(data);
        }
    }
}
