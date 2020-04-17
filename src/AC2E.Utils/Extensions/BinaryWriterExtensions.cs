using AC2E.Crypto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AC2E.Utils.Extensions {

    public static class BinaryWriterExtensions {

        public static void WriteEncryptedString(this BinaryWriter writer, string str, Encoding encoding = null) {
            if (encoding == null) {
                encoding = Encoding.ASCII;
            }
            ushort numChars = (ushort)str.Length;
            writer.Write(numChars);
            if (numChars == 0) {
                writer.Align();
                return;
            }
            byte[] bytes = encoding.GetBytes(str);
            CryptoUtil.encrypt(bytes, 0, bytes.Length);
            writer.Write(bytes);
            writer.Align();
        }

        public static void WriteList<T>(this BinaryWriter writer, List<T> list, Action<BinaryWriter, T> elementWriter, uint sizeOfSize = 4) {
            if (sizeOfSize == 1) {
                writer.Write((byte)list.Count);
            } else if (sizeOfSize == 2) {
                writer.Write((ushort)list.Count);
            } else if (sizeOfSize == 4) {
                writer.Write((uint)list.Count);
            } else {
                throw new Exception();
            }
            foreach (var element in list) {
                elementWriter.Invoke(writer, element);
            }
        }

        public static void WriteDictionary<K, V>(this BinaryWriter writer, Dictionary<K, V> dict, Action<BinaryWriter, K> keyWriter, Action<BinaryWriter, V> valueWriter) {
            writer.Write((ushort)dict.Count);
            writer.Write((ushort)0);
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
}
