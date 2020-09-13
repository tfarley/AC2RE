namespace AC2E.Def {

    public class DaggerTemplate : WeaponTemplate {

        public override PackageType packageType => PackageType.DaggerTemplate;

        public DaggerTemplate(AC2Reader data) : base(data) {

        }
    }
}
