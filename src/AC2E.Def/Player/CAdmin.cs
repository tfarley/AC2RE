namespace AC2E.Def {

    public class CAdmin : CPlayer {

        public override PackageType packageType => PackageType.CAdmin;

        public ARHash<IPackage> logHash; // m_hashLog

        public CAdmin(AC2Reader data) : base(data) {
            data.ReadPkg<ARHash<IPackage>>(v => logHash = v);
        }
    }
}
