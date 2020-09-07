using System.Numerics;

namespace AC2E.Def {

    public struct PositionOffset {

        public CellId cell; // m_cid
        public Vector3 offset; // m_offset

        public PositionOffset(AC2Reader data) {
            cell = data.ReadCellId();
            offset = data.ReadVector();
        }
    }
}
