namespace AC2E.Def {

    public class ToolTemplate : Tool {

        public override PackageType packageType => PackageType.ToolTemplate;

        public ToolTemplate(AC2Reader data) : base(data) {

        }
    }
}
