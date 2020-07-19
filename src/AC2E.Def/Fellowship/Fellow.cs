using System.IO;

namespace AC2E.Def {

    public class Fellow : IPackage {

        public PackageType packageType => PackageType.Fellow;

        public double m_join_ts;
        public uint m_level;
        public FellowVitals m_vitals;
        public StringInfo m_name;

        public Fellow() {

        }

        public Fellow(BinaryReader data, PackageRegistry registry) {
            m_join_ts = data.ReadDouble();
            m_level = data.ReadUInt32();
            data.ReadPkgRef<FellowVitals>(v => m_vitals = v, registry);
            data.ReadPkgRef<StringInfo>(v => m_name = v, registry);
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(m_join_ts);
            data.Write(m_level);
            data.Write(m_vitals, registry);
            data.Write(m_name, registry);
        }
    }
}
