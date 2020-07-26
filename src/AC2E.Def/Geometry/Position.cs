﻿namespace AC2E.Def {

    public class Position : IPackage {

        public NativeType nativeType => NativeType.POSITION;

        public CellId cell; // objcell_id
        public Frame frame; // frame

        public Position() {

        }

        public Position(AC2Reader data) {
            cell = data.ReadCellId();
            frame = new Frame(data);
        }

        public void write(AC2Writer data) {
            data.Write(cell);
            frame.write(data);
        }
    }
}
