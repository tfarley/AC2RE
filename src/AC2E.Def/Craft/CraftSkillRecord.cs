namespace AC2E.Def {

    public class CraftSkillRecord : IPackage {

        public PackageType packageType => PackageType.CraftSkillRecord;

        public ulong m_uliXPEarnedToday;
        public DataId m_didCraftSkill;
        public ulong m_uliAvailableCraftXP;
        public int m_iLevel;

        public CraftSkillRecord() {

        }

        public CraftSkillRecord(AC2Reader data) {
            m_uliXPEarnedToday = data.ReadUInt64();
            m_didCraftSkill = data.ReadDataId();
            m_uliAvailableCraftXP = data.ReadUInt64();
            m_iLevel = data.ReadInt32();
        }

        public void write(AC2Writer data) {
            data.Write(m_uliXPEarnedToday);
            data.Write(m_didCraftSkill);
            data.Write(m_uliAvailableCraftXP);
            data.Write(m_iLevel);
        }
    }
}
