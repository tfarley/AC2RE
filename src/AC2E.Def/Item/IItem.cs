namespace AC2E.Def {

    public class IItem : CItem {

        public override PackageType packageType => PackageType.IItem;

        public IItem(AC2Reader data) : base(data) {

        }
    }
}
