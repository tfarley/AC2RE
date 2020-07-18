using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class ConsignerDescPkg : IPackage {

        public PackageType packageType => PackageType.ConsignerDesc;

        public StringInfo m_siLocation;
        public RList<ConsignmentPkg> m_consignments;
        public DataId m_didCatalog;

        public ConsignerDescPkg() {

        }

        public ConsignerDescPkg(BinaryReader data, PackageRegistry registry) {
            data.ReadPkgRef<StringInfo>(v => m_siLocation = v, registry);
            data.ReadPkgRef<RList<IPackage>>(v => m_consignments = v.to<ConsignmentPkg>(), registry);
            m_didCatalog = data.ReadDataId();
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(m_siLocation, registry);
            data.Write(m_consignments, registry);
            data.Write(m_didCatalog);
        }
    }
}
