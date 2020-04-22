using System.IO;

namespace AC2E.Utils.Extensions {

    public static class BinaryWriterExtensions {

        public static void Align(this BinaryWriter writer, uint bytes) {
            long alignDelta = writer.BaseStream.Position % bytes;
            if (alignDelta != 0) {
                for (long i = 0; i < bytes - alignDelta; i++) {
                    writer.Write((byte)0);
                }
            }
        }
    }
}
