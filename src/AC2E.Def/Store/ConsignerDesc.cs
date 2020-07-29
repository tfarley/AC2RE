namespace AC2E.Def {

    public class ConsignerDesc : IPackage {

        public PackageType packageType => PackageType.ConsignerDesc;

        public StringInfo locationName; // m_siLocation
        public RList<Consignment> consignments; // m_consignments
        public DataId catalogDid; // m_didCatalog

        public ConsignerDesc() {

        }

        public ConsignerDesc(AC2Reader data) {
            data.ReadPkg<StringInfo>(v => locationName = v);
            data.ReadPkg<RList<IPackage>>(v => consignments = v.to<Consignment>());
            catalogDid = data.ReadDataId();
        }

        public void write(AC2Writer data) {
            data.WritePkg(locationName);
            data.WritePkg(consignments);
            data.Write(catalogDid);
        }
    }
}
