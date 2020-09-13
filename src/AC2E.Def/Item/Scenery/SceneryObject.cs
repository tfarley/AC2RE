namespace AC2E.Def {

    public class SceneryObject : IItem {

        public override PackageType packageType => PackageType.SceneryObject;

        public SceneryObject(AC2Reader data) : base(data) {

        }
    }
}
