namespace AC2E.Def {

    public class CWeapon : Weapon {

        public override PackageType packageType => PackageType.CWeapon;

        public CWeapon(AC2Reader data) : base(data) {

        }
    }
}
