namespace AC2RE.Definitions {

    public class PathExternalLink {

        public uint portalId; // m_portal_id
        public uint otherCellId; // m_other_cell_id
        public uint otherPortalId; // m_other_portal_id
        public uint nodeId; // m_node_id
        public Ray linkPortal; // m_link_portal
        public float linkHeight; // m_link_height

        public PathExternalLink(AC2Reader data) {
            portalId = data.ReadUInt32();
            otherCellId = data.ReadUInt32();
            otherPortalId = data.ReadUInt32();
            nodeId = data.ReadUInt32();
            linkPortal = new(data);
            linkHeight = data.ReadSingle();
        }
    }
}
