namespace AC2E.Def {

    // TODO: Make this generic for the ARHash?
    public class SimpleMasterList : IPackage {

        public virtual PackageType packageType => PackageType.SimpleMasterList;

        public ARHash<IPackage> map; // mMap

        public SimpleMasterList(AC2Reader data) {
            data.ReadPkg<ARHash<IPackage>>(v => map = v);
        }
    }
}
