namespace AC2E.Def {

    public class IWeapon : CWeapon {

        public override PackageType packageType => PackageType.IWeapon;

        public IWeapon(AC2Reader data) : base(data) {

        }
    }
}
