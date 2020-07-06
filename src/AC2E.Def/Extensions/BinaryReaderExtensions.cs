using AC2E.Def;
using AC2E.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AC2E {

    public static class BinaryReaderExtensions {

        public static int UnpackInt32(this BinaryReader reader) {
            if ((PackTag)reader.ReadUInt32() != PackTag.INT) {
                throw new InvalidDataException();
            }

            return reader.ReadInt32();
        }

        public static uint UnpackUInt32(this BinaryReader reader) {
            if ((PackTag)reader.ReadUInt32() != PackTag.INT) {
                throw new InvalidDataException();
            }

            return reader.ReadUInt32();
        }

        public static long UnpackInt64(this BinaryReader reader) {
            if ((PackTag)reader.ReadUInt32() != PackTag.LONGINT) {
                throw new InvalidDataException();
            }

            return reader.ReadInt64();
        }

        public static ulong UnpackUInt64(this BinaryReader reader) {
            if ((PackTag)reader.ReadUInt32() != PackTag.LONGINT) {
                throw new InvalidDataException();
            }

            return reader.ReadUInt64();
        }

        public static float UnpackSingle(this BinaryReader reader) {
            if ((PackTag)reader.ReadUInt32() != PackTag.FLOAT) {
                throw new InvalidDataException();
            }

            return reader.ReadSingle();
        }

        public static double UnpackDouble(this BinaryReader reader) {
            if ((PackTag)reader.ReadUInt32() != PackTag.LONGFLOAT) {
                throw new InvalidDataException();
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
            AC2Crypto.decrypt(bytes, 0, length);
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
                throw new ArgumentException();
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

        public static Dictionary<K, V> ReadStlMap<K, V>(this BinaryReader reader, Func<K> keyReader, Func<V> valueReader) {
            // Variation of dictionary where the count is a full 32 bits without any table size (used for std::map specifically, see STREAMPACK_STL)
            Dictionary<K, V> dict = new Dictionary<K, V>();
            uint numElements = reader.ReadUInt32();
            for (int i = 0; i < numElements; i++) {
                dict.Add(keyReader.Invoke(), valueReader.Invoke());
            }
            return dict;
        }

        public static InstanceId ReadInstanceId(this BinaryReader reader) {
            return new InstanceId(reader.ReadUInt64());
        }

        public static InstanceIdWithStamp ReadInstanceIdWithStamp(this BinaryReader reader) {
            return new InstanceIdWithStamp {
                id = reader.ReadInstanceId(),
                instanceStamp = reader.ReadUInt16(),
                otherStamp = reader.ReadUInt16(),
            };
        }

        public static Vector ReadVector(this BinaryReader reader) {
            return new Vector {
                x = reader.ReadSingle(),
                y = reader.ReadSingle(),
                z = reader.ReadSingle(),
            };
        }

        public static Quaternion ReadQuaternion(this BinaryReader reader) {
            return new Quaternion {
                x = reader.ReadSingle(),
                y = reader.ReadSingle(),
                z = reader.ReadSingle(),
                w = reader.ReadSingle(),
            };
        }

        public static RGBAColor ReadRGBAColor(this BinaryReader reader) {
            return new RGBAColor {
                r = reader.ReadSingle(),
                g = reader.ReadSingle(),
                b = reader.ReadSingle(),
                a = reader.ReadSingle(),
            };
        }

        public static Heading ReadHeading(this BinaryReader reader) {
            return new Heading(((reader.ReadUInt32() >> 24) & 0x000000FF) / 255.0f * 360.0f);
        }

        public static CellId ReadCellId(this BinaryReader reader) {
            return new CellId(reader.ReadUInt32());
        }
    }
}
