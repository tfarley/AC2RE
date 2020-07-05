using AC2E.Interp;
using System.Collections.Generic;
using System.IO;

namespace AC2E.WLib {

    public class ContainerSegmentDescriptorPkg : IPackage {

        public NativeType nativeType => NativeType.UNDEF;
        public PackageType packageType => PackageType.ContainerSegmentDescriptor;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAPOBJECT);

        public uint id { get; set; }

        public uint mSegmentMaxSize;
        public uint mSegmentSize;

        public void write(BinaryWriter data, List<IPackage> references) {
            data.Write(mSegmentMaxSize);
            data.Write(mSegmentSize);
        }
    }
}
