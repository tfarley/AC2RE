namespace AC2E.Def {

    public class ConsignerDesc : IPackage {

        public PackageType packageType => PackageType.ConsignerDesc;

        public StringInfo m_siLocation;
        public RList<Consignment> m_consignments;
        public DataId m_didCatalog;

        public ConsignerDesc() {

        }

        public ConsignerDesc(AC2Reader data, PackageRegistry registry) {
            data.ReadPkgRef<StringInfo>(v => m_siLocation = v, registry);
            data.ReadPkgRef<RList<IPackage>>(v => m_consignments = v.to<Consignment>(), registry);
            m_didCatalog = data.ReadDataId();
        }

        public void write(AC2Writer data, PackageRegistry registry) {
            data.Write(m_siLocation, registry);
            data.Write(m_consignments, registry);
            data.Write(m_didCatalog);
        }
    }
}
