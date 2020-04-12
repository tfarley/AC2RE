using System.IO;
using System.Text;

public static class BinaryReaderExtensions {

    public static string ReadEncryptedString(this BinaryReader binaryReader) {
        ushort length = binaryReader.ReadUInt16();
        byte[] bytes = binaryReader.ReadBytes(length);
        binaryReader.Align();
        CryptoUtil.decrypt(bytes, 0, length);
        return Encoding.ASCII.GetString(bytes);
    }

    public static void Align(this BinaryReader reader) {
        long alignDelta = reader.BaseStream.Position % 4;
        if (alignDelta != 0) {
            reader.BaseStream.Position += 4 - alignDelta;
        }
    }
}
