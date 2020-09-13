namespace AC2E.Def {

    public class JewelryTemplate : Jewelry {

        public override PackageType packageType => PackageType.JewelryTemplate;

        public JewelryTemplate(AC2Reader data) : base(data) {

        }
    }
}
