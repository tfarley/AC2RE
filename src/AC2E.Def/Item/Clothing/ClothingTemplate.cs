namespace AC2E.Def {

    public class ClothingTemplate : IClothing {

        public override PackageType packageType => PackageType.ClothingTemplate;

        public ClothingTemplate(AC2Reader data) : base(data) {

        }
    }
}
