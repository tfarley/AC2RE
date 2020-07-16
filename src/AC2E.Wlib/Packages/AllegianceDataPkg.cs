using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class AllegianceDataPkg : IPackage {

        public PackageType packageType => PackageType.AllegianceData;

        public ulong m_xp_passed;
        public SpeciesType m_species;
        public uint m_level;
        public WPString m_userDefinedTitle;
        public SexType m_sex;
        public ulong m_xp_pooled;
        public bool m_fIsOfficer;
        public InstanceId m_id;
        public uint m_rank;
        public uint m_factionType;
        public StringInfo m_name;

        public AllegianceDataPkg() {

        }

        public AllegianceDataPkg(BinaryReader data, PackageRegistry registry) {
            m_xp_passed = data.ReadUInt64();
            m_species = (SpeciesType)data.ReadUInt32();
            m_level = data.ReadUInt32();
            data.ReadPkgRef<WPString>(v => m_userDefinedTitle = v, registry);
            m_sex = (SexType)data.ReadUInt32();
            m_xp_pooled = data.ReadUInt64();
            m_fIsOfficer = data.ReadUInt32() != 0;
            m_id = data.ReadInstanceId();
            m_rank = data.ReadUInt32();
            m_factionType = data.ReadUInt32();
            data.ReadPkgRef<StringInfo>(v => m_name = v, registry);
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(m_xp_passed);
            data.Write((uint)m_species);
            data.Write(m_level);
            data.Write(m_userDefinedTitle, registry);
            data.Write((uint)m_sex);
            data.Write(m_xp_pooled);
            data.Write(m_fIsOfficer ? (uint)1 : (uint)0);
            data.Write(m_id);
            data.Write(m_rank);
            data.Write(m_factionType);
            data.Write(m_name, registry);
        }
    }
}
