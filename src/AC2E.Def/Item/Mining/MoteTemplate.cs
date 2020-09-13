namespace AC2E.Def {

    public class MoteTemplate : IItem {

        public override PackageType packageType => PackageType.MoteTemplate;

        public MoteTemplate(AC2Reader data) : base(data) {

        }
    }
}
