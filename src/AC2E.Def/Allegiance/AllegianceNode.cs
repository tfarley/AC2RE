namespace AC2E.Def {

    public class AllegianceNode : IPackage {

        public PackageType packageType => PackageType.AllegianceNode;

        public RList<AllegianceNode> m_vassalNodes;
        public AllegianceNode m_patron;
        public AllegianceData m_data;
        public AllegianceNode m_vassal;
        public AllegianceNode m_peer;

        public AllegianceNode() {

        }

        public AllegianceNode(AC2Reader data, PackageRegistry registry) {
            data.ReadPkgRef<RList<IPackage>>(v => m_vassalNodes = v.to<AllegianceNode>(), registry);
            data.ReadPkgRef<AllegianceNode>(v => m_patron = v, registry);
            data.ReadPkgRef<AllegianceData>(v => m_data = v, registry);
            data.ReadPkgRef<AllegianceNode>(v => m_vassal = v, registry);
            data.ReadPkgRef<AllegianceNode>(v => m_peer = v, registry);
        }

        public void write(AC2Writer data, PackageRegistry registry) {
            data.Write(m_vassalNodes, registry);
            data.Write(m_patron, registry);
            data.Write(m_data, registry);
            data.Write(m_vassal, registry);
            data.Write(m_peer, registry);
        }
    }
}
