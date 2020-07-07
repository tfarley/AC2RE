using AC2E.Def;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public class AHashSetPkg : IPackage {

        public NativeType nativeType => NativeType.AHASHSET;
        public PackageType packageType => PackageType.UNDEF;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAPOBJECT);

        public PackageId id { get; set; }

        public HashSet<uint> contents;

        public void write(BinaryWriter data, List<IPackage> references) {
            // TODO: Write correct format
        }
    }
}
