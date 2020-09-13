namespace AC2E.Def {

    public class ArmorTemplate : ClothingTemplate {

        public override PackageType packageType => PackageType.ArmorTemplate;

        public ArmorTemplate(AC2Reader data) : base(data) {

        }
    }
}
