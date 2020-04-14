using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public static class BinaryReaderExtensions {

    public static string ReadEncryptedString(this BinaryReader reader, Encoding encoding = null) {
        if (encoding == null) {
            encoding = Encoding.ASCII;
        }
        ushort numChars = reader.ReadUInt16();
        int length = encoding.GetMaxByteCount(numChars - 1);
        byte[] bytes = reader.ReadBytes(length);
        reader.Align();
        CryptoUtil.decrypt(bytes, 0, length);
        return encoding.GetString(bytes);
    }

    public static Dictionary<K, V> ReadDictionary<K, V>(this BinaryReader reader, Func<BinaryReader, K> keyReader, Func<BinaryReader, V> valueReader) {
        Dictionary<K, V> dict = new Dictionary<K, V>();
        ushort numElements = reader.ReadUInt16();
        ushort tableSize = reader.ReadUInt16();
        for (int i = 0; i < numElements; i++) {
            dict.Add(keyReader.Invoke(reader), valueReader.Invoke(reader));
        }
        return dict;
    }

    public static void Align(this BinaryReader reader) {
        long alignDelta = reader.BaseStream.Position % 4;
        if (alignDelta != 0) {
            reader.BaseStream.Position += 4 - alignDelta;
        }
    }
}
