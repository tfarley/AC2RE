namespace AC2E.Def {

    public class MountTable : IPackage {

        public virtual PackageType packageType => PackageType.MountTable;

        public ARHash<IPackage> mountTable; // mMountTable

        public MountTable(AC2Reader data) {
            data.ReadPkg<ARHash<IPackage>>(v => mountTable = v);
        }
    }
}
