namespace AC2E.Def {

    public class CItem : Item {

        public override PackageType packageType => PackageType.CItem;

        public CItem(AC2Reader data) : base(data) {

        }
    }
}
