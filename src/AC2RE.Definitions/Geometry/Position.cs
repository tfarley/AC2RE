namespace AC2RE.Definitions {

    public class Position : IPackage {

        public NativeType nativeType => NativeType.Position;

        public CellId cell; // objcell_id
        public Frame frame; // frame

        public Position() {

        }

        public Position(AC2Reader data) {
            cell = data.ReadCellId();
            frame = new(data);
        }

        public void write(AC2Writer data) {
            data.Write(cell);
            frame.write(data);
        }
    }
}
