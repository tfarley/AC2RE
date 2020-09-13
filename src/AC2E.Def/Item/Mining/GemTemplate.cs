namespace AC2E.Def {

    public class GemTemplate : IItem {

        public override PackageType packageType => PackageType.GemTemplate;

        public GemTemplate(AC2Reader data) : base(data) {

        }
    }
}
