namespace AC2E.Def {

    public class Item : gmCEntity {

        public override PackageType packageType => PackageType.Item;

        public Item(AC2Reader data) : base(data) {

        }
    }
}
