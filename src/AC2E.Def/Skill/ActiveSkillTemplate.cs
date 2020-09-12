namespace AC2E.Def {

    public class ActiveSkillTemplate : ActiveSkill {

        public override PackageType packageType => PackageType.ActiveSkillTemplate;

        public ActiveSkillTemplate(AC2Reader data) : base(data) {

        }
    }
}
