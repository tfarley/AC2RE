namespace AC2E.Def {

    public struct PositionOffset {

        public CellId cell; // m_cid
        public Vector offset; // m_offset

        public PositionOffset(AC2Reader data) {
            cell = data.ReadCellId();
            offset = data.ReadVector();
        }
    }
}
