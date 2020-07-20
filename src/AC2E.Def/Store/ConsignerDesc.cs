namespace AC2E.Def {

    public class ConsignerDesc : IPackage {

        public PackageType packageType => PackageType.ConsignerDesc;

        public StringInfo m_siLocation;
        public RList<Consignment> m_consignments;
        public DataId m_didCatalog;

        public ConsignerDesc() {

        }

        public ConsignerDesc(AC2Reader data) {
            data.ReadPkg<StringInfo>(v => m_siLocation = v);
            data.ReadPkg<RList<IPackage>>(v => m_consignments = v.to<Consignment>());
            m_didCatalog = data.ReadDataId();
        }

        public void write(AC2Writer data) {
            data.WritePkg(m_siLocation);
            data.WritePkg(m_consignments);
            data.Write(m_didCatalog);
        }
    }
}
