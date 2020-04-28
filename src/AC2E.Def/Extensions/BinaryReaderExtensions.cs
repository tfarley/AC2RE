using AC2E.Crypto;
using AC2E.Def.Enums;
using AC2E.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AC2E.Def.Extensions {

    public static class BinaryReaderExtensions {

        public static int UnpackInt32(this BinaryReader reader) {
            if ((PackTag)reader.ReadUInt32() != PackTag.INT) {
                throw new Exception();
            }

            return reader.ReadInt32();
        }

        public static uint UnpackUInt32(this BinaryReader reader) {
            if ((PackTag)reader.ReadUInt32() != PackTag.INT) {
                throw new Exception();
            }

            return reader.ReadUInt32();
        }

        public static long UnpackInt64(this BinaryReader reader) {
            if ((PackTag)reader.ReadUInt32() != PackTag.LONGINT) {
                throw new Exception();
            }

            return reader.ReadInt64();
        }

        public static ulong UnpackUInt64(this BinaryReader reader) {
            if ((PackTag)reader.ReadUInt32() != PackTag.LONGINT) {
                throw new Exception();
            }

            return reader.ReadUInt64();
        }

        public static float UnpackSingle(this BinaryReader reader) {
            if ((PackTag)reader.ReadUInt32() != PackTag.FLOAT) {
                throw new Exception();
            }

            return reader.ReadSingle();
        }

        public static double UnpackDouble(this BinaryReader reader) {
            if ((PackTag)reader.ReadUInt32() != PackTag.LONGFLOAT) {
                throw new Exception();
            }

            return reader.ReadDouble();
        }

        public static string ReadNullTermString(this BinaryReader reader, Encoding encoding = null) {
            if (encoding == null) {
                encoding = Encoding.ASCII;
            }
            MemoryStream buffer = new MemoryStream();
            using (BinaryWriter data = new BinaryWriter(buffer)) {
                byte val;
                do {
                    val = reader.ReadByte();
                    data.Write(val);
                } while (val != 0);
            }
            byte[] bytes = buffer.ToArray();
            return encoding.GetString(bytes, 0, bytes.Length - 1);
        }

        public static string ReadEncryptedString(this BinaryReader reader, Encoding encoding = null) {
            if (encoding == null) {
                encoding = Encoding.ASCII;
            }
            ushort numChars = reader.ReadUInt16();
            if (numChars == 0) {
                reader.Align(4);
                return "";
            }
            int length = numChars * encoding.GetMaxByteCount(0);
            byte[] bytes = reader.ReadBytes(length);
            reader.Align(4);
            CryptoUtil.decrypt(bytes, 0, length);
            return encoding.GetString(bytes);
        }

        public static List<T> ReadList<T>(this BinaryReader reader, Func<T> elementReader, uint sizeOfSize = 4) {
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
                list.Add(elementReader.Invoke());
            }
            return list;
        }

        public static Dictionary<K, V> ReadDictionary<K, V>(this BinaryReader reader, Func<K> keyReader, Func<V> valueReader) {
            Dictionary<K, V> dict = new Dictionary<K, V>();
            ushort numElements = reader.ReadUInt16();
            ushort tableSize = reader.ReadUInt16();
            for (int i = 0; i < numElements; i++) {
                dict.Add(keyReader.Invoke(), valueReader.Invoke());
            }
            return dict;
        }
    }
}
