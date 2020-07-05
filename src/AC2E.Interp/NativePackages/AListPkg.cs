using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public class AListPkg : IPackage {

        public NativeType nativeType => NativeType.ALIST;
        public PackageType packageType => PackageType.UNDEF;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAPOBJECT);

        public uint id { get; set; }

        public List<uint> contents;

        public void write(BinaryWriter data, List<IPackage> references) {
            data.Write(contents, data.Write);
        }
    }
}
