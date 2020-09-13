namespace AC2E.Def {

    public class HandTemplate : WeaponTemplate {

        public override PackageType packageType => PackageType.HandTemplate;

        public HandTemplate(AC2Reader data) : base(data) {

        }
    }
}
