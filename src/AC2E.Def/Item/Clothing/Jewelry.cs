namespace AC2E.Def {

    public class Jewelry : CItem {

        public override PackageType packageType => PackageType.Jewelry;

        public Jewelry(AC2Reader data) : base(data) {

        }
    }
}
