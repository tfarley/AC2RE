namespace AC2E.Def {

    public class Mine : CItem {

        public override PackageType packageType => PackageType.Mine;

        public Mine(AC2Reader data) : base(data) {

        }
    }
}
