namespace AC2E.Def {

    public class Key : CItem {

        public override PackageType packageType => PackageType.Key;

        public Key(AC2Reader data) : base(data) {

        }
    }
}
