namespace AC2E.Def {

    public class ContainerSegmentDescriptor : IPackage {

        public PackageType packageType => PackageType.ContainerSegmentDescriptor;

        public uint mSegmentMaxSize;
        public uint mSegmentSize;

        public ContainerSegmentDescriptor() {

        }

        public ContainerSegmentDescriptor(AC2Reader data) {
            mSegmentMaxSize = data.ReadUInt32();
            mSegmentSize = data.ReadUInt32();
        }

        public void write(AC2Writer data) {
            data.Write(mSegmentMaxSize);
            data.Write(mSegmentSize);
        }
    }
}
