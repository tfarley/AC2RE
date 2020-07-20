namespace AC2E.Def {

    public class AllegianceData : IPackage {

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

        public AllegianceData() {

        }

        public AllegianceData(AC2Reader data) {
            m_xp_passed = data.ReadUInt64();
            m_species = (SpeciesType)data.ReadUInt32();
            m_level = data.ReadUInt32();
            data.ReadPkg<WPString>(v => m_userDefinedTitle = v);
            m_sex = (SexType)data.ReadUInt32();
            m_xp_pooled = data.ReadUInt64();
            m_fIsOfficer = data.ReadBoolean();
            m_id = data.ReadInstanceId();
            m_rank = data.ReadUInt32();
            m_factionType = data.ReadUInt32();
            data.ReadPkg<StringInfo>(v => m_name = v);
        }

        public void write(AC2Writer data) {
            data.Write(m_xp_passed);
            data.Write((uint)m_species);
            data.Write(m_level);
            data.WritePkg(m_userDefinedTitle);
            data.Write((uint)m_sex);
            data.Write(m_xp_pooled);
            data.Write(m_fIsOfficer);
            data.Write(m_id);
            data.Write(m_rank);
            data.Write(m_factionType);
            data.WritePkg(m_name);
        }
    }
}
