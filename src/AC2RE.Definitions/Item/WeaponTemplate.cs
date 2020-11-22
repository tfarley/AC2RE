namespace AC2RE.Definitions {

    public class WeaponTemplate : gmCEntity {

        public override PackageType packageType => PackageType.WeaponTemplate;

        public int balanceLevel; // _balanceLevel

        public WeaponTemplate(AC2Reader data) : base(data) {
            balanceLevel = data.ReadInt32();
        }
    }
}
