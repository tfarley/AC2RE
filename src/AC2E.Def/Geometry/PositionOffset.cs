namespace AC2E.Def {

    public class PositionOffset {

        public CellId cellId; // m_cid
        public Vector offset; // m_offset

        public PositionOffset(AC2Reader data) {
            cellId = data.ReadCellId();
            offset = data.ReadVector();
        }
    }
}
