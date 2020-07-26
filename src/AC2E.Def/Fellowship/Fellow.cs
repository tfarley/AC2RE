namespace AC2E.Def {

    public class Fellow : IPackage {

        public PackageType packageType => PackageType.Fellow;

        public double joinTime; // m_join_ts
        public uint level; // m_level
        public FellowVitals vitals; // m_vitals
        public StringInfo name; // m_name

        public Fellow() {

        }

        public Fellow(AC2Reader data) {
            joinTime = data.ReadDouble();
            level = data.ReadUInt32();
            data.ReadPkg<FellowVitals>(v => vitals = v);
            data.ReadPkg<StringInfo>(v => name = v);
        }

        public void write(AC2Writer data) {
            data.Write(joinTime);
            data.Write(level);
            data.WritePkg(vitals);
            data.WritePkg(name);
        }
    }
}
