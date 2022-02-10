namespace AC2RE.Definitions;

public class CraftSkillRecord : IHeapObject {

    public PackageType packageType => PackageType.CraftSkillRecord;

    public ulong xpEarnedToday; // m_uliXPEarnedToday
    public DataId craftSkillDid; // m_didCraftSkill
    public ulong availableCraftXp; // m_uliAvailableCraftXP
    public int level; // m_iLevel

    public CraftSkillRecord() {

    }

    public CraftSkillRecord(AC2Reader data) {
        xpEarnedToday = data.ReadUInt64();
        craftSkillDid = data.ReadDataId();
        availableCraftXp = data.ReadUInt64();
        level = data.ReadInt32();
    }

    public void write(AC2Writer data) {
        data.Write(xpEarnedToday);
        data.Write(craftSkillDid);
        data.Write(availableCraftXp);
        data.Write(level);
    }
}
