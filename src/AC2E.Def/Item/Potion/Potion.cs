namespace AC2E.Def {

    public class Potion : CItem {

        public override PackageType packageType => PackageType.Potion;

        public Potion(AC2Reader data) : base(data) {

        }
    }
}
