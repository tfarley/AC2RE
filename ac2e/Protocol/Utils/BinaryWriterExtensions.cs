using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public static class BinaryWriterExtensions {

    public static void WriteEncryptedString(this BinaryWriter writer, string str, Encoding encoding = null) {
        if (encoding == null) {
            encoding = Encoding.ASCII;
        }
        ushort numChars = (ushort)str.Length;
        writer.Write(numChars);
        byte[] bytes = encoding.GetBytes(str);
        CryptoUtil.encrypt(bytes, 0, bytes.Length);
        writer.Write(bytes);
        writer.Align();
    }

    public static void WriteDictionary<K, V>(this BinaryWriter writer, Dictionary<K, V> dict, Action<BinaryWriter, K> keyWriter, Action<BinaryWriter, V> valueWriter) {
        ushort numElements = (ushort)dict.Count;
        writer.Write(numElements);
        writer.Write(numElements);
        foreach (var element in dict) {
            keyWriter.Invoke(writer, element.Key);
            valueWriter.Invoke(writer, element.Value);
        }
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
