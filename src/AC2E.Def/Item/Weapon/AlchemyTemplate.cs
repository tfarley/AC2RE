namespace AC2E.Def {

    public class AlchemyTemplate : WeaponTemplate {

        public override PackageType packageType => PackageType.AlchemyTemplate;

        public AlchemyTemplate(AC2Reader data) : base(data) {

        }
    }
}
