using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public static class BinaryReaderExtensions {

    public static string ReadNullTermString(this BinaryReader reader, Encoding encoding = null) {
        if (encoding == null) {
            encoding = Encoding.ASCII;
        }
        MemoryStream data = new MemoryStream();
        using (BinaryWriter writer = new BinaryWriter(data)) {
            byte val;
            do {
                val = reader.ReadByte();
                writer.Write(val);
            } while (val != 0);
        }
        byte[] bytes = data.ToArray();
        return encoding.GetString(bytes, 0, bytes.Length - 1);
    }

    public static string ReadEncryptedString(this BinaryReader reader, Encoding encoding = null) {
        if (encoding == null) {
            encoding = Encoding.ASCII;
        }
        ushort numChars = reader.ReadUInt16();
        if (numChars == 0) {
            reader.Align();
            return "";
        }
        int length = numChars * encoding.GetMaxByteCount(0);
        byte[] bytes = reader.ReadBytes(length);
        reader.Align();
        CryptoUtil.decrypt(bytes, 0, length);
        return encoding.GetString(bytes);
    }

    public static List<T> ReadList<T>(this BinaryReader reader, Func<BinaryReader, T> elementReader, uint sizeOfSize = 4) {
        List<T> list = new List<T>();
        uint numElements;
        if (sizeOfSize == 1) {
            numElements = reader.ReadByte();
        } else if (sizeOfSize == 2) {
            numElements = reader.ReadUInt16();
        } else if (sizeOfSize == 4) {
            numElements = reader.ReadUInt32();
        } else {
            throw new Exception();
        }
        for (int i = 0; i < numElements; i++) {
            list.Add(elementReader.Invoke(reader));
        }
        return list;
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
