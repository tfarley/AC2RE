using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public class AAHashPkg : IPackage {

        public NativeType nativeType => NativeType.AAHASH;
        public PackageType packageType => PackageType.UNDEF;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAPOBJECT);

        public uint id { get; set; }

        public Dictionary<uint, uint> contents;

        public void write(BinaryWriter data, List<IPackage> references) {
            data.Write(contents, data.Write, data.Write);
        }
    }
}
