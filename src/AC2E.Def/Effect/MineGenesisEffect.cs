namespace AC2E.Def {

    public class MineGenesisEffect : GenesisEffect {

        public override PackageType packageType => PackageType.MineGenesisEffect;

        public DataId craftSkillDid; // m_didCraftSkill
        public int craftSkillRating; // m_craftSkillRating

        public MineGenesisEffect(AC2Reader data) : base(data) {
            craftSkillDid = data.ReadDataId();
            craftSkillRating = data.ReadInt32();
        }
    }
}
