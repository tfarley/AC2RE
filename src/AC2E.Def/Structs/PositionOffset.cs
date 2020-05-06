using AC2E.Def.Extensions;
using System.IO;

namespace AC2E.Def.Structs {

    public class PositionOffset {

        public CellId cellId; // m_cid
        public Vector offset; // m_offset

        public PositionOffset(BinaryReader data) {
            cellId = data.ReadCellId();
            offset = data.ReadVector();
        }
    }
}
