using System.IO;

namespace AC2E.Utils {

    public static class BinaryReaderExtensions {

        public static void Align(this BinaryReader reader, uint bytes) {
            long alignDelta = reader.BaseStream.Position % bytes;
            if (alignDelta != 0) {
                reader.BaseStream.Position += bytes - alignDelta;
            }
        }
    }
}
