using AC2E.Crypto;
using AC2E.Def.Enums;
using AC2E.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AC2E.Def.Extensions {

    public static class BinaryWriterExtensions {

        public static void Pack(this BinaryWriter writer, int value) {
            writer.Write((uint)PackTag.INT);
            writer.Write(value);
        }

        public static void Pack(this BinaryWriter writer, uint value) {
            writer.Write((uint)PackTag.INT);
            writer.Write(value);
        }

        public static void Pack(this BinaryWriter writer, long value) {
            writer.Write((uint)PackTag.LONGINT);
            writer.Write(value);
        }

        public static void Pack(this BinaryWriter writer, ulong value) {
            writer.Write((uint)PackTag.LONGINT);
            writer.Write(value);
        }

        public static void Pack(this BinaryWriter writer, float value) {
            writer.Write((uint)PackTag.FLOAT);
            writer.Write(value);
        }

        public static void Pack(this BinaryWriter writer, double value) {
            writer.Write((uint)PackTag.LONGFLOAT);
            writer.Write(value);
        }

        public static void WriteEncryptedString(this BinaryWriter writer, string str, Encoding encoding = null) {
            if (encoding == null) {
                encoding = Encoding.ASCII;
            }
            ushort numChars = (ushort)str.Length;
            writer.Write(numChars);
            if (numChars == 0) {
                writer.Align(4);
                return;
            }
            byte[] bytes = encoding.GetBytes(str);
            CryptoUtil.encrypt(bytes, 0, bytes.Length);
            writer.Write(bytes);
            writer.Align(4);
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
    }
}
