using System.IO;

namespace AC2E.Def {

    public class Position {

        public CellId cellId; // objcell_id
        public Frame frame; // frame

        public Position(BinaryReader data) {
            cellId = data.ReadCellId();
            frame = new Frame(data);
        }
    }
}
