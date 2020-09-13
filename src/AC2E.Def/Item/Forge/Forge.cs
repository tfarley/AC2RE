namespace AC2E.Def {

    public class Forge : CItem {

        public override PackageType packageType => PackageType.Forge;

        public Forge(AC2Reader data) : base(data) {

        }
    }
}
