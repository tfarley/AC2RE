namespace AC2E.Def {

    public class EntityFilter : IPackage {

        public PackageType packageType => PackageType.EntityFilter;

        public DataIdList entityDids; // m_entityDIDs

        public EntityFilter(AC2Reader data) {
            data.ReadPkg<AList>(v => entityDids = new DataIdList(v));
        }
    }
}
