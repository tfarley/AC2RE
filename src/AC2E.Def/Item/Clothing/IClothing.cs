namespace AC2E.Def {

    public class IClothing : Clothing {

        public override PackageType packageType => PackageType.IClothing;

        public IClothing(AC2Reader data) : base(data) {

        }
    }
}
