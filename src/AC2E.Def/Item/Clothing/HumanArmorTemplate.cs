namespace AC2E.Def {

    public class HumanArmorTemplate : ArmorTemplate {

        public override PackageType packageType => PackageType.HumanArmorTemplate;

        public HumanArmorTemplate(AC2Reader data) : base(data) {

        }
    }
}
