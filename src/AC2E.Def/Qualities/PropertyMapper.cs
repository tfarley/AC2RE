namespace AC2E.Def {

    public class PropertyMapper : IPackage {

        public PackageType packageType => PackageType.PropertyMapper;

        public AAHash propServer; // m_hashPropServer
        public AAHash propClient; // m_hashPropClient

        public PropertyMapper(AC2Reader data) {
            data.ReadPkg<AAHash>(v => propServer = v);
            data.ReadPkg<AAHash>(v => propClient = v);
        }
    }
}
