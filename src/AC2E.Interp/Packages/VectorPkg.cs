using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp.Packages {

    public class VectorPkg : IPackage {

        public NativeType nativeType => NativeType.VECTOR;
        public PackageType packageType => PackageType.UNDEF;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAP_OBJECT);

        public uint id { get; set; }

        public float x;
        public float y;
        public float z;

        public void write(BinaryWriter data, List<IPackage> references) {
            data.Write(x);
            data.Write(y);
            data.Write(z);
        }
    }
}
