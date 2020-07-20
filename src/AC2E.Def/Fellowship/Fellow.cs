namespace AC2E.Def {

    public class Fellow : IPackage {

        public PackageType packageType => PackageType.Fellow;

        public double m_join_ts;
        public uint m_level;
        public FellowVitals m_vitals;
        public StringInfo m_name;

        public Fellow() {

        }

        public Fellow(AC2Reader data) {
            m_join_ts = data.ReadDouble();
            m_level = data.ReadUInt32();
            data.ReadPkg<FellowVitals>(v => m_vitals = v);
            data.ReadPkg<StringInfo>(v => m_name = v);
        }

        public void write(AC2Writer data) {
            data.Write(m_join_ts);
            data.Write(m_level);
            data.WritePkg(m_vitals);
            data.WritePkg(m_name);
        }
    }
}
