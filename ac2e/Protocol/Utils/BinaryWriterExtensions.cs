using System.IO;
using System.Text;

public static class BinaryWriterExtensions {

    public static void WriteEncryptedString(this BinaryWriter writer, string str) {
        ushort length = (ushort)str.Length;
        writer.Write(length);
        byte[] bytes = Encoding.ASCII.GetBytes(str);
        CryptoUtil.encrypt(bytes, 0, length);
        writer.Write(bytes);
        writer.Align();
    }

    public static void Align(this BinaryWriter writer) {
        long alignDelta = writer.BaseStream.Position % 4;
        if (alignDelta != 0) {
            for (long i = 0; i < 4 - alignDelta; i++) {
                writer.Write((byte)0);
            }
        }
    }
}
