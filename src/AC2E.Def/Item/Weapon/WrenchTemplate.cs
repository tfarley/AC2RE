namespace AC2E.Def {

    public class WrenchTemplate : WeaponTemplate {

        public override PackageType packageType => PackageType.WrenchTemplate;

        public WrenchTemplate(AC2Reader data) : base(data) {

        }
    }
}
