namespace AC2E.Def {

    public class AllegianceNode : IPackage {

        public PackageType packageType => PackageType.AllegianceNode;

        public RList<AllegianceNode> vassalNodes; // m_vassalNodes
        public AllegianceNode patron; // m_patron
        public AllegianceData allegianceData; // m_data
        public AllegianceNode vassal; // m_vassal
        public AllegianceNode peer; // m_peer

        public AllegianceNode() {

        }

        public AllegianceNode(AC2Reader data) {
            data.ReadPkg<RList<IPackage>>(v => vassalNodes = v.to<AllegianceNode>());
            data.ReadPkg<AllegianceNode>(v => patron = v);
            data.ReadPkg<AllegianceData>(v => allegianceData = v);
            data.ReadPkg<AllegianceNode>(v => vassal = v);
            data.ReadPkg<AllegianceNode>(v => peer = v);
        }

        public void write(AC2Writer data) {
            data.WritePkg(vassalNodes);
            data.WritePkg(patron);
            data.WritePkg(allegianceData);
            data.WritePkg(vassal);
            data.WritePkg(peer);
        }
    }
}
