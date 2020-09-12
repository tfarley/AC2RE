namespace AC2E.Def {

    public class EntityFilter : StoreFilter {

        public override PackageType packageType => PackageType.EntityFilter;

        public DataIdList entityDids; // m_entityDIDs

        public EntityFilter(AC2Reader data) : base(data) {
            data.ReadPkg<AList>(v => entityDids = new DataIdList(v));
        }
    }
}
