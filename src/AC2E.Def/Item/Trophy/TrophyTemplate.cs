namespace AC2E.Def {

    public class TrophyTemplate : IItem {

        public override PackageType packageType => PackageType.TrophyTemplate;

        public TrophyTemplate(AC2Reader data) : base(data) {

        }
    }
}
