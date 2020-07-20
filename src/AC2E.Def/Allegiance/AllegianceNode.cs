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

        public AllegianceNode(AC2Reader data) {
            data.ReadPkg<RList<IPackage>>(v => m_vassalNodes = v.to<AllegianceNode>());
            data.ReadPkg<AllegianceNode>(v => m_patron = v);
            data.ReadPkg<AllegianceData>(v => m_data = v);
            data.ReadPkg<AllegianceNode>(v => m_vassal = v);
            data.ReadPkg<AllegianceNode>(v => m_peer = v);
        }

        public void write(AC2Writer data) {
            data.WritePkg(m_vassalNodes);
            data.WritePkg(m_patron);
            data.WritePkg(m_data);
            data.WritePkg(m_vassal);
            data.WritePkg(m_peer);
        }
    }
}
