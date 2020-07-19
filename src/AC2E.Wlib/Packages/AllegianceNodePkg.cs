using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class AllegianceNodePkg : IPackage {

        public PackageType packageType => PackageType.AllegianceNode;

        public RList<AllegianceNodePkg> m_vassalNodes;
        public AllegianceNodePkg m_patron;
        public AllegianceDataPkg m_data;
        public AllegianceNodePkg m_vassal;
        public AllegianceNodePkg m_peer;

        public AllegianceNodePkg() {

        }

        public AllegianceNodePkg(BinaryReader data, PackageRegistry registry) {
            data.ReadPkgRef<RList<IPackage>>(v => m_vassalNodes = v.to<AllegianceNodePkg>(), registry);
            data.ReadPkgRef<AllegianceNodePkg>(v => m_patron = v, registry);
            data.ReadPkgRef<AllegianceDataPkg>(v => m_data = v, registry);
            data.ReadPkgRef<AllegianceNodePkg>(v => m_vassal = v, registry);
            data.ReadPkgRef<AllegianceNodePkg>(v => m_peer = v, registry);
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(m_vassalNodes, registry);
            data.Write(m_patron, registry);
            data.Write(m_data, registry);
            data.Write(m_vassal, registry);
            data.Write(m_peer, registry);
        }
    }
}
