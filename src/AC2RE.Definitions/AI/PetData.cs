namespace AC2RE.Definitions {

    public class PetData : IPackage {

        public PackageType packageType => PackageType.PetData;

        public double timeLeftWorld; // m_timeLeftWorld
        public uint petClass; // m_class
        public uint flags; // m_flags

        public PetData() {

        }

        public PetData(AC2Reader data) {
            timeLeftWorld = data.ReadDouble();
            petClass = data.ReadUInt32();
            flags = data.ReadUInt32();
        }

        public void write(AC2Writer data) {
            data.Write(timeLeftWorld);
            data.Write(petClass);
            data.Write(flags);
        }
    }
}
