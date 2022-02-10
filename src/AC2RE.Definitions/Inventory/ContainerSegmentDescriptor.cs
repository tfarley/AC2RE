namespace AC2RE.Definitions;

public class ContainerSegmentDescriptor : IHeapObject {

    public PackageType packageType => PackageType.ContainerSegmentDescriptor;

    public uint segmentMaxSize; // mSegmentMaxSize
    public uint segmentSize; // mSegmentSize

    public ContainerSegmentDescriptor() {

    }

    public ContainerSegmentDescriptor(AC2Reader data) {
        segmentMaxSize = data.ReadUInt32();
        segmentSize = data.ReadUInt32();
    }

    public void write(AC2Writer data) {
        data.Write(segmentMaxSize);
        data.Write(segmentSize);
    }
}
