namespace AC2E.Def {

    public class PerkTemplate : PerkSkill {

        public override PackageType packageType => PackageType.PerkTemplate;

        public PerkTemplate(AC2Reader data) : base(data) {

        }
    }
}
