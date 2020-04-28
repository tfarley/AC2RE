using AC2E.Interp.Extensions;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp.Packages {

    public class GMQuestInfoPkg : IPackage {

        public NativeType nativeType => NativeType.GMQUESTINFO;
        public PackageType packageType => PackageType.UNDEF;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAP_OBJECT);

        public uint id { get; set; }

        // TODO: Fill out

        public void write(BinaryWriter data, List<IPackage> references) {

        }
    }
}
