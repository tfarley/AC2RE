namespace AC2E.Def {

    public class Tool : CItem {

        public override PackageType packageType => PackageType.Tool;

        public Tool(AC2Reader data) : base(data) {

        }
    }
}
