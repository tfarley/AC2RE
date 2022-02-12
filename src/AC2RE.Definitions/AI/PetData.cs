namespace AC2RE.Definitions;

public class PetData : IHeapObject {

    public PackageType packageType => PackageType.PetData;

    public double timeLeftWorld; // m_timeLeftWorld
    public AIPetClass petClass; // m_class
    public uint flags; // m_flags

    public PetData() {

    }

    public PetData(AC2Reader data) {
        timeLeftWorld = data.ReadDouble();
        petClass = data.ReadEnum<AIPetClass>();
        flags = data.ReadUInt32();
    }

    public void write(AC2Writer data) {
        data.Write(timeLeftWorld);
        data.WriteEnum(petClass);
        data.Write(flags);
    }
}
