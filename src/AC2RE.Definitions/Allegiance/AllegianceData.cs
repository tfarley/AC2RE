namespace AC2RE.Definitions;

public class AllegianceData : IHeapObject {

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
        species = data.ReadEnum<SpeciesType>();
        level = data.ReadUInt32();
        data.ReadHO<WPString>(v => userDefinedTitle = v);
        sex = data.ReadEnum<SexType>();
        xpPooled = data.ReadUInt64();
        isOfficer = data.ReadBoolean();
        id = data.ReadInstanceId();
        rank = data.ReadUInt32();
        factionType = data.ReadEnum<FactionType>();
        data.ReadHO<StringInfo>(v => name = v);
    }

    public void write(AC2Writer data) {
        data.Write(xpPassed);
        data.WriteEnum(species);
        data.Write(level);
        data.WriteHO(userDefinedTitle);
        data.WriteEnum(sex);
        data.Write(xpPooled);
        data.Write(isOfficer);
        data.Write(id);
        data.Write(rank);
        data.WriteEnum(factionType);
        data.WriteHO(name);
    }
}
