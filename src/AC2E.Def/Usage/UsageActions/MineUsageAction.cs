namespace AC2E.Def {

    public class MineUsageAction : MasterListMember {

        public override PackageType packageType => PackageType.MineUsageAction;

        public DataId craftSkillIronDid; // m_didCraftSkillIron
        public DataId craftSkillStoneDid; // m_didCraftSkillStone
        public DataId craftSkillCrystalDid; // m_didCraftSkillCrystal
        public DataId craftSkillSilverDid; // m_didCraftSkillSilver
        public DataId craftSkillWoodDid; // m_didCraftSkillWood

        public MineUsageAction(AC2Reader data) : base(data) {
            craftSkillIronDid = data.ReadDataId();
            craftSkillStoneDid = data.ReadDataId();
            craftSkillCrystalDid = data.ReadDataId();
            craftSkillSilverDid = data.ReadDataId();
            craftSkillWoodDid = data.ReadDataId();
        }
    }
}
