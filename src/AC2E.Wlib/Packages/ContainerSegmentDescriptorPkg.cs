using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class ContainerSegmentDescriptorPkg : IPackage {

        public PackageType packageType => PackageType.ContainerSegmentDescriptor;

        public uint mSegmentMaxSize;
        public uint mSegmentSize;

        public ContainerSegmentDescriptorPkg() {

        }

        public ContainerSegmentDescriptorPkg(BinaryReader data) {
            mSegmentMaxSize = data.ReadUInt32();
            mSegmentSize = data.ReadUInt32();
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(mSegmentMaxSize);
            data.Write(mSegmentSize);
        }
    }
}
