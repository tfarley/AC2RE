namespace AC2E.Def {

    public class GenericPortal : Portal {

        public override PackageType packageType => PackageType.GenericPortal;

        public GenericPortal(AC2Reader data) : base(data) {

        }
    }
}
