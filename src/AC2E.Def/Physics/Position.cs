using System.IO;

namespace AC2E.Def {

    public class Position : IPackage {

        public NativeType nativeType => NativeType.POSITION;

        public CellId cellId; // objcell_id
        public Frame frame; // frame

        public Position() {

        }

        public Position(BinaryReader data) {
            cellId = data.ReadCellId();
            frame = new Frame(data);
        }

        public void write(BinaryWriter data) {
            data.Write(cellId);
            frame.write(data);
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            write(data);
        }
    }
}
