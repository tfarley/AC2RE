namespace AC2E.Def {

    public class ShieldTemplate : IShield {

        public override PackageType packageType => PackageType.ShieldTemplate;

        public ShieldTemplate(AC2Reader data) : base(data) {

        }
    }
}
