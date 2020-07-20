namespace AC2E.Def {

    public class PetData : IPackage {

        public PackageType packageType => PackageType.PetData;

        public double m_timeLeftWorld;
        public uint m_class;
        public uint m_flags;

        public PetData() {

        }

        public PetData(AC2Reader data) {
            m_timeLeftWorld = data.ReadDouble();
            m_class = data.ReadUInt32();
            m_flags = data.ReadUInt32();
        }

        public void write(AC2Writer data, PackageRegistry registry) {
            data.Write(m_timeLeftWorld);
            data.Write(m_class);
            data.Write(m_flags);
        }
    }
}
