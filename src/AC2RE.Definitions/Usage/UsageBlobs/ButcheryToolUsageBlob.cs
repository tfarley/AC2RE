namespace AC2RE.Definitions;

public class ButcheryToolUsageBlob : UsageBlob {

    public override PackageType packageType => PackageType.ButcheryToolUsageBlob;

    public float toolQuantityMod; // m_fToolQuantityMod
    public StringInfo corpseName; // m_siCorpseName
    public DataId profileDid; // m_didProfile
    public DataId craftSkillDid; // m_didCraftSkill
    public StringInfo playerName; // m_siPlayerName
    public float toolXpMod; // m_fToolXPMod
    public CorpsePermissionBlob blob; // m_blob

    public ButcheryToolUsageBlob() : base() {

    }

    public ButcheryToolUsageBlob(AC2Reader data) : base(data) {
        toolQuantityMod = data.ReadSingle();
        data.ReadPkg<StringInfo>(v => corpseName = v);
        profileDid = data.ReadDataId();
        craftSkillDid = data.ReadDataId();
        data.ReadPkg<StringInfo>(v => playerName = v);
        toolXpMod = data.ReadSingle();
        data.ReadPkg<CorpsePermissionBlob>(v => blob = v);
    }

    public override void write(AC2Writer data) {
        base.write(data);
        data.Write(toolQuantityMod);
        data.WritePkg(corpseName);
        data.Write(profileDid);
        data.Write(craftSkillDid);
        data.WritePkg(playerName);
        data.Write(toolXpMod);
        data.WritePkg(blob);
    }
}
