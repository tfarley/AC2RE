using AC2E.Def;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public class VectorPkg : IPackage {

        public NativeType nativeType => NativeType.VECTOR;
        public PackageType packageType => PackageType.UNDEF;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAPOBJECT);

        public uint id { get; set; }

        public Vector contents;

        public void write(BinaryWriter data, List<IPackage> references) {
            data.Write(contents);
        }
    }
}
