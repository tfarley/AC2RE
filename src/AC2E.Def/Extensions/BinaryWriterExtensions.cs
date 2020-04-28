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

        public static void Write<T>(this BinaryWriter writer, List<T> list, Action<T> elementWriter, uint sizeOfSize = 4) {
            int count = list != null ? list.Count : 0;
            if (sizeOfSize == 1) {
                writer.Write((byte)count);
            } else if (sizeOfSize == 2) {
                writer.Write((ushort)count);
            } else if (sizeOfSize == 4) {
                writer.Write((uint)count);
            } else {
                throw new Exception();
            }
            if (count > 0) {
                foreach (var element in list) {
                    elementWriter.Invoke(element);
                }
            }
        }

        public static void Write<K, V>(this BinaryWriter writer, Dictionary<K, V> dict, Action<K> keyWriter, Action<V> valueWriter) {
            int count = dict != null ? dict.Count : 0;
            writer.Write((ushort)count);
            writer.Write(count > 0 ? (ushort)0x200 : (ushort)0);
            if (count > 0) {
                foreach (var element in dict) {
                    keyWriter.Invoke(element.Key);
                    valueWriter.Invoke(element.Value);
                }
            }
        }
    }
}
