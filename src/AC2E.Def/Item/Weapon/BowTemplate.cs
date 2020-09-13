namespace AC2E.Def {

    public class BowTemplate : WeaponTemplate {

        public override PackageType packageType => PackageType.BowTemplate;

        public BowTemplate(AC2Reader data) : base(data) {

        }
    }
}
