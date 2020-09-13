namespace AC2E.Def {

    public class TrumpTemplate : IItem {

        public override PackageType packageType => PackageType.TrumpTemplate;

        public TrumpTemplate(AC2Reader data) : base(data) {

        }
    }
}
