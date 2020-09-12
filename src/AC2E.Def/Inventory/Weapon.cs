namespace AC2E.Def {

    public class Weapon : CItem {

        public override PackageType packageType => PackageType.Weapon;

        public Weapon(AC2Reader data) : base(data) {

        }
    }
}
