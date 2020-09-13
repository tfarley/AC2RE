namespace AC2E.Def {

    public class MapTemplate : IItem {

        public override PackageType packageType => PackageType.MapTemplate;

        public MapTemplate(AC2Reader data) : base(data) {

        }
    }
}
