namespace AC2E.Def {

    public class Coin : CItem {

        public override PackageType packageType => PackageType.Coin;

        public Coin(AC2Reader data) : base(data) {

        }
    }
}
