﻿using AC2E.Def;
using AC2E.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AC2E {

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

        public static void Pack(this BinaryWriter writer, InstanceId value) {
            writer.Write((uint)PackTag.LONGINT);
            writer.Write(value);
        }

        public static void WriteEncryptedString(this BinaryWriter writer, string str, Encoding encoding = null) {
            if (encoding == null) {
                encoding = Encoding.ASCII;
            }
            int numChars = str != null ? str.Length : 0;
            writer.Write((ushort)numChars);
            if (numChars == 0) {
                writer.Align(4);
                return;
            }
            byte[] bytes = encoding.GetBytes(str);
            AC2Crypto.encrypt(bytes, 0, bytes.Length);
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
                throw new ArgumentException();
            }
            if (count > 0) {
                foreach (var element in list) {
                    elementWriter.Invoke(element);
                }
            }
        }

        public static void Write<T>(this BinaryWriter writer, HashSet<T> set, Action<T> elementWriter) {
            int count = set != null ? set.Count : 0;
            writer.Write((ushort)count);
            // NOTE: The client may crash if this table size value is too small, even if the set itself does not have many entries
            writer.Write((ushort)Math.Max(count, 0x800));
            if (count > 0) {
                foreach (var element in set) {
                    elementWriter.Invoke(element);
                }
            }
        }

        public static void WriteMulti<K, V>(this BinaryWriter writer, Dictionary<K, List<V>> dict, Action<K> keyWriter, Action<V> valueWriter) {
            int count = dict != null ? dict.Count : 0;
            writer.Write(count);
            if (count > 0) {
                foreach (var element in dict) {
                    foreach (var subelement in element.Value) {
                        keyWriter.Invoke(element.Key);
                        valueWriter.Invoke(subelement);
                    }
                }
            }
        }

        public static void Write<K, V>(this BinaryWriter writer, Dictionary<K, V> dict, Action<K> keyWriter, Action<V> valueWriter) {
            int count = dict != null ? dict.Count : 0;
            writer.Write((ushort)count);
            // NOTE: The client may crash if this table size value is too small, even if the dictionary itself does not have many entries
            writer.Write((ushort)Math.Max(count, 0x800));
            if (count > 0) {
                foreach (var element in dict) {
                    keyWriter.Invoke(element.Key);
                    valueWriter.Invoke(element.Value);
                }
            }
        }

        public static void WriteStlMap<K, V>(this BinaryWriter writer, Dictionary<K, V> dict, Action<K> keyWriter, Action<V> valueWriter) {
            // Variation of dictionary where the count is a full 32 bits without any table size (used for std::map specifically, see STREAMPACK_STL)
            int count = dict != null ? dict.Count : 0;
            writer.Write(count);
            if (count > 0) {
                foreach (var element in dict) {
                    keyWriter.Invoke(element.Key);
                    valueWriter.Invoke(element.Value);
                }
            }
        }

        public static void Write(this BinaryWriter writer, PackageId value) {
            writer.Write(value.id);
        }

        public static void Write(this BinaryWriter writer, InstanceId value) {
            writer.Write(value.id);
        }

        public static void Write(this BinaryWriter writer, InstanceIdWithStamp value) {
            writer.Write(value.id);
            writer.Write(value.instanceStamp);
            writer.Write(value.otherStamp);
        }

        public static void Write(this BinaryWriter writer, Vector value) {
            writer.Write(value.x);
            writer.Write(value.y);
            writer.Write(value.z);
        }

        public static void Write(this BinaryWriter writer, Quaternion value) {
            writer.Write(value.x);
            writer.Write(value.y);
            writer.Write(value.z);
            writer.Write(value.w);
        }

        public static void Write(this BinaryWriter writer, RGBAColor value) {
            writer.Write((byte)(value.r * 255.0f));
            writer.Write((byte)(value.g * 255.0f));
            writer.Write((byte)(value.b * 255.0f));
            writer.Write((byte)(value.a * 255.0f));
        }

        public static void Write(this BinaryWriter writer, Heading value) {
            writer.Write((uint)(value.rotDegrees / 360.0f * 255.0f) & 0x000000FF);
        }

        public static void Write(this BinaryWriter writer, CellId value) {
            writer.Write(value.id);
        }
    }
}
