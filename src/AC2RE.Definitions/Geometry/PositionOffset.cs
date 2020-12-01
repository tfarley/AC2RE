using System.Numerics;

namespace AC2RE.Definitions {

    public struct PositionOffset {

        public CellId cell; // m_cid
        public Vector3 offset; // m_offset

        public PositionOffset(CellId cell, Vector3 offset) {
            this.cell = cell;
            this.offset = offset;
        }

        public PositionOffset(AC2Reader data) {
            cell = data.ReadCellId();
            offset = data.ReadVector();
        }

        public void write(AC2Writer data) {
            data.Write(cell);
            data.Write(offset);
        }
    }
}
