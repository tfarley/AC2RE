namespace AC2E.Def {

    public class PassiveSkillTemplate : Skill {

        public override PackageType packageType => PackageType.PassiveSkillTemplate;

        public PassiveSkillTemplate(AC2Reader data) : base(data) {

        }
    }
}
