using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp.Packages {

    public class AHashSetPkg : IPackage {

        public NativeType nativeType => NativeType.AHASHSET;
        public PackageType packageType => PackageType.UNDEF;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAPOBJECT);

        public uint id { get; set; }

        public HashSet<uint> contents;

        public void write(BinaryWriter data, List<IPackage> references) {
            // TODO: Write correct format
        }
    }
}
