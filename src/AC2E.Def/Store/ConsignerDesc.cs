namespace AC2E.Def {

    public class ConsignerDesc : IPackage {

        public PackageType packageType => PackageType.ConsignerDesc;

        public StringInfo locationname; // m_siLocation
        public RList<Consignment> consignments; // m_consignments
        public DataId catalogDid; // m_didCatalog

        public ConsignerDesc() {

        }

        public ConsignerDesc(AC2Reader data) {
            data.ReadPkg<StringInfo>(v => locationname = v);
            data.ReadPkg<RList<IPackage>>(v => consignments = v.to<Consignment>());
            catalogDid = data.ReadDataId();
        }

        public void write(AC2Writer data) {
            data.WritePkg(locationname);
            data.WritePkg(consignments);
            data.Write(catalogDid);
        }
    }
}
