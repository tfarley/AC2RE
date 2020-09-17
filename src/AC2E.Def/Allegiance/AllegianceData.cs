namespace AC2E.Def {

    public class AllegianceData : IPackage {

        public PackageType packageType => PackageType.AllegianceData;

        public ulong xpPassed; // m_xp_passed
        public SpeciesType species; // m_species
        public uint level; // m_level
        public WPString userDefinedTitle; // m_userDefinedTitle
        public SexType sex; // m_sex
        public ulong xpPooled; // m_xp_pooled
        public bool isOfficer; // m_fIsOfficer
        public InstanceId id; // m_id
        public uint rank; // m_rank
        public FactionType factionType; // m_factionType
        public StringInfo name; // m_name

        public AllegianceData() {

        }

        public AllegianceData(AC2Reader data) {
            xpPassed = data.ReadUInt64();
            species = (SpeciesType)data.ReadUInt32();
            level = data.ReadUInt32();
            data.ReadPkg<WPString>(v => userDefinedTitle = v);
            sex = (SexType)data.ReadUInt32();
            xpPooled = data.ReadUInt64();
            isOfficer = data.ReadBoolean();
            id = data.ReadInstanceId();
            rank = data.ReadUInt32();
            factionType = (FactionType)data.ReadUInt32();
            data.ReadPkg<StringInfo>(v => name = v);
        }

        public void write(AC2Writer data) {
            data.Write(xpPassed);
            data.Write((uint)species);
            data.Write(level);
            data.WritePkg(userDefinedTitle);
            data.Write((uint)sex);
            data.Write(xpPooled);
            data.Write(isOfficer);
            data.Write(id);
            data.Write(rank);
            data.Write((uint)factionType);
            data.WritePkg(name);
        }
    }
}
