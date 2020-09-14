namespace AC2E.Def {

    public class HeroControl : IPackage {

        public PackageType packageType => PackageType.HeroControl;

        public DataId heroSkillCreditTokenDid; // m_didHeroSkillCreditToken

        public HeroControl(AC2Reader data) {
            heroSkillCreditTokenDid = data.ReadDataId();
        }
    }
}
