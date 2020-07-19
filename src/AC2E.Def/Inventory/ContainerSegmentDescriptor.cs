using System.IO;

namespace AC2E.Def {

    public class ContainerSegmentDescriptor : IPackage {

        public PackageType packageType => PackageType.ContainerSegmentDescriptor;

        public uint mSegmentMaxSize;
        public uint mSegmentSize;

        public ContainerSegmentDescriptor() {

        }

        public ContainerSegmentDescriptor(BinaryReader data) {
            mSegmentMaxSize = data.ReadUInt32();
            mSegmentSize = data.ReadUInt32();
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(mSegmentMaxSize);
            data.Write(mSegmentSize);
        }
    }
}
