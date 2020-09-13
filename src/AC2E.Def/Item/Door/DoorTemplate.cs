namespace AC2E.Def {

    public class DoorTemplate : Door {

        public override PackageType packageType => PackageType.DoorTemplate;

        public DoorTemplate(AC2Reader data) : base(data) {

        }
    }
}
