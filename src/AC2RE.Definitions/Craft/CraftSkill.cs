using System.Collections.Generic;

namespace AC2RE.Definitions;

public class CraftSkill : MasterDIDListMember {

    public override PackageType packageType => PackageType.CraftSkill;

    public uint advancementCap; // m_uiAdvancementCap
    public StringInfo name; // m_siName
    public Dictionary<uint, uint> titleHash; // m_titleHash
    public StringInfo description; // m_siDesc
    public DataId advancementTableDid; // m_didAdvancementTable
    public DataId iconDid; // m_didIcon
    public DataId dailyXpTableDid; // m_didDailyXPTable
    public float genericXpMod; // m_fGenericXPMod
    public bool cannotRaise; // m_bCannotRaise
    public StringInfo commonName; // m_siCommonName
    public DataId defaultXpTableDid; // m_didDefaultXPTable

    public CraftSkill(AC2Reader data) : base(data) {
        advancementCap = data.ReadUInt32();
        data.ReadHO<StringInfo>(v => name = v);
        data.ReadHO<AAHash>(v => titleHash = v);
        data.ReadHO<StringInfo>(v => description = v);
        advancementTableDid = data.ReadDataId();
        iconDid = data.ReadDataId();
        dailyXpTableDid = data.ReadDataId();
        genericXpMod = data.ReadSingle();
        cannotRaise = data.ReadBoolean();
        data.ReadHO<StringInfo>(v => commonName = v);
        defaultXpTableDid = data.ReadDataId();
    }
}
