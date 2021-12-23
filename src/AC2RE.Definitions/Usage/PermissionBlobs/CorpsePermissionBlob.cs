namespace AC2RE.Definitions;

public class CorpsePermissionBlob : IPackage {

    public PackageType packageType => PackageType.CorpsePermissionBlob;

    public bool lootProof; // m_lootProof
    public bool hasBeenOpened; // m_hasBeenOpened
    public uint level; // m_level
    public bool unlockAfterFirstLoot; // m_unlockAfterFirstLoot
    public DataId butcheryProfileDid; // m_didButcheryProfile
    public float lootTimer; // m_lootTimer
    public InstanceId originatorId; // m_originator
    public InstanceId killerId; // m_killer
    public DataId butcheryCraftSkillDid; // m_didButcheryCraftSkill
    public InstanceId claimantId; // m_claimant
    public bool hasBeenButchered; // m_hasBeenButchered
    public double timeOfDeath; // m_timeOfDeath

    public CorpsePermissionBlob(AC2Reader data) {
        lootProof = data.ReadBoolean();
        hasBeenOpened = data.ReadBoolean();
        level = data.ReadUInt32();
        unlockAfterFirstLoot = data.ReadBoolean();
        butcheryProfileDid = data.ReadDataId();
        lootTimer = data.ReadSingle();
        originatorId = data.ReadInstanceId();
        killerId = data.ReadInstanceId();
        butcheryCraftSkillDid = data.ReadDataId();
        claimantId = data.ReadInstanceId();
        hasBeenButchered = data.ReadBoolean();
        timeOfDeath = data.ReadDouble();
    }
}
