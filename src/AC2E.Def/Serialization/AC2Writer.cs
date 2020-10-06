using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;

namespace AC2E.Def {

    public class AC2Writer : BinaryWriter {

        private static readonly byte UNINITIALIZED_DATA = 0xCD;

        public readonly PackageRegistry packageRegistry;

        public AC2Writer(Stream output) : base(output) {
            packageRegistry = new PackageRegistry();
        }

        public AC2Writer(Stream output, PackageRegistry packageRegistry) : base(output) {
            this.packageRegistry = packageRegistry;
        }

        public override void Write(string value) {
            Write(value, Encoding.ASCII);
        }

        public override void Write(bool value) {
            Write((uint)(value ? 1 : 0));
        }

        // NOTE: Special naming to avoid stomping on ushort overload
        private void WritePackTag(PackTag value) {
            Write((uint)value);
        }

        public void Pack(bool value) {
            Pack((uint)(value ? 1 : 0));
        }

        public void Pack(int value) {
            WritePackTag(PackTag.INT);
            Write(value);
        }

        public void Pack(uint value) {
            WritePackTag(PackTag.INT);
            Write(value);
        }

        public void Pack(long value) {
            WritePackTag(PackTag.LONGINT);
            Write(value);
        }

        public void Pack(ulong value) {
            WritePackTag(PackTag.LONGINT);
            Write(value);
        }

        public void Pack(float value) {
            WritePackTag(PackTag.FLOAT);
            Write(value);
        }

        public void Pack(double value) {
            WritePackTag(PackTag.LONGFLOAT);
            Write(value);
        }

        public void Pack(InstanceId value) {
            Pack(value.id);
        }

        public void Pack(DataId value) {
            Pack(value.id);
        }

        public void Pack(IPackage value) {
            packageRegistry.references.Add(value);

            MemoryStream buffer = new MemoryStream();
            using (AC2Writer data = new AC2Writer(buffer, packageRegistry)) {
                for (int i = 0; i < packageRegistry.references.Count; i++) {
                    IPackage referencedPackage = packageRegistry.references[i];
                    if (referencedPackage != null) {
                        writePackage(data, referencedPackage);
                    }
                }
            }

            for (int i = packageRegistry.references.Count - 1; i >= 0; i--) {
                if (packageRegistry.references[i] == null) {
                    packageRegistry.references.RemoveAt(i);
                }
            }

            WritePackTag(PackTag.PACKAGE);
            Write(packageRegistry.references, v => Write(packageRegistry.getId(v)));
            Write(buffer.ToArray());

            packageRegistry.references.Clear();
        }

        private void writePackage(AC2Writer data, IPackage value) {
            InterpReferenceMeta referenceMeta = packageRegistry.getMeta(value).referenceMeta;

            data.Write(referenceMeta.id);

            if (referenceMeta.isSingleton) {
                value.write(data);
            } else {
                data.Write((uint)0);
                data.Write((ushort)value.nativeType);
                data.Write(value.nativeType != NativeType.UNDEF ? (ushort)0xFFFF : (ushort)value.packageType);
                if (value.nativeType != NativeType.UNDEF) {
                    value.write(data);
                } else {
                    // Placeholder for length
                    data.Write((uint)0);

                    long contentStart = data.BaseStream.Position;
                    value.write(data);
                    long contentEnd = data.BaseStream.Position;
                    long contentLength = contentEnd - contentStart;
                    data.BaseStream.Seek(-contentLength - sizeof(uint), SeekOrigin.Current);
                    data.Write((uint)contentLength / sizeof(uint));
                    data.BaseStream.Seek(contentEnd, SeekOrigin.Begin);
                }

                // TODO: Still not sure this is the correct condition for whether there are references or not
                // Write out field descriptions
                if (value.nativeType == NativeType.UNDEF) {
                    foreach (FieldDesc fieldDesc in InterpMeta.getFieldDescs(value.GetType())) {
                        data.Write((byte)fieldDesc.fieldType);
                        if (fieldDesc.numWords == 2) {
                            data.Write(UNINITIALIZED_DATA);
                        }
                    }
                    // TODO: Should this align occur even if there are no references?
                    data.Align(4);
                } else {
                    // TODO: Is this needed/correct?
                    data.Write((uint)0);
                }
            }
        }

        public void WritePkg<T>(T value) where T : IPackage {
            if (value != null) {
                Write(packageRegistry.getId(value));
                packageRegistry.references.Add(value);
            } else {
                Write(PackageId.NULL);
            }
        }

        public void WritePkgFullRef<T>(T value) where T : IPackage {
            if (value != null) {
                new ReferenceId(packageRegistry.getId(value)).write(this);
                packageRegistry.references.Add(value);
            } else {
                new ReferenceId(PackageId.NULL).write(this);
            }
        }

        public void Write(string str, Encoding encoding) {
            int numChars = str != null ? str.Length : 0;
            Write((ushort)numChars);
            if (numChars == 0) {
                Align(4);
                return;
            }
            byte[] bytes = encoding.GetBytes(str);
            AC2Crypto.encrypt(bytes, 0, bytes.Length);
            Write(bytes);
            Align(4);
        }

        public void Write<T>(List<T> list, Action<T> elementWriter, uint sizeOfSize = 4) {
            int count = list != null ? list.Count : 0;
            if (sizeOfSize == 1) {
                Write((byte)count);
            } else if (sizeOfSize == 2) {
                Write((ushort)count);
            } else if (sizeOfSize == 4) {
                Write((uint)count);
            } else {
                throw new ArgumentException();
            }
            if (count > 0) {
                foreach (var element in list) {
                    elementWriter.Invoke(element);
                }
            }
        }

        public void Write<T>(HashSet<T> set, Action<T> elementWriter) {
            int count = set != null ? set.Count : 0;
            Write((ushort)count);
            // NOTE: The client may crash if this table size value is too small, even if the set itself does not have many entries
            Write((ushort)Math.Max(count, 0x800));
            if (count > 0) {
                foreach (var element in set) {
                    elementWriter.Invoke(element);
                }
            }
        }

        public void WriteMulti<K, V>(Dictionary<K, List<V>> dict, Action<K> keyWriter, Action<V> valueWriter) {
            int count = dict != null ? dict.Count : 0;
            Write(count);
            if (count > 0) {
                foreach (var element in dict) {
                    foreach (var subelement in element.Value) {
                        keyWriter.Invoke(element.Key);
                        valueWriter.Invoke(subelement);
                    }
                }
            }
        }

        public void Write<K, V>(Dictionary<K, V> dict, Action<K> keyWriter, Action<V> valueWriter) {
            int count = dict != null ? dict.Count : 0;
            Write((ushort)count);
            // NOTE: The client may crash if this table size value is too small, even if the dictionary itself does not have many entries
            Write((ushort)Math.Max(count, 0x800));
            if (count > 0) {
                foreach (var element in dict) {
                    keyWriter.Invoke(element.Key);
                    valueWriter.Invoke(element.Value);
                }
            }
        }

        public void WriteStlMap<K, V>(Dictionary<K, V> dict, Action<K> keyWriter, Action<V> valueWriter) {
            // Variation of dictionary where the count is a full 32 bits without any table size (used for std::map specifically, see STREAMPACK_STL)
            int count = dict != null ? dict.Count : 0;
            Write(count);
            if (count > 0) {
                foreach (var element in dict) {
                    keyWriter.Invoke(element.Key);
                    valueWriter.Invoke(element.Value);
                }
            }
        }

        public void Write(PackageId value) {
            Write(value.id);
        }

        public void Write(InstanceId value) {
            Write(value.id);
        }

        public void Write(InstanceIdWithStamp value) {
            Write(value.id);
            Write(value.instanceStamp);
            Write(value.otherStamp);
        }

        public void Write(Vector3 value) {
            Write(value.X);
            Write(value.Y);
            Write(value.Z);
        }

        public void Write(Quaternion value) {
            Write(value.W);
            Write(value.X);
            Write(value.Y);
            Write(value.Z);
        }

        public void Write(Matrix4x4 value) {
            Write(value.M11);
            Write(value.M12);
            Write(value.M13);
            Write(value.M14);
            Write(value.M21);
            Write(value.M22);
            Write(value.M23);
            Write(value.M24);
            Write(value.M31);
            Write(value.M32);
            Write(value.M33);
            Write(value.M34);
            Write(value.M41);
            Write(value.M42);
            Write(value.M43);
            Write(value.M44);
        }

        public void Write(RGBAColor value) {
            Write((byte)(value.r * 255.0f));
            Write((byte)(value.g * 255.0f));
            Write((byte)(value.b * 255.0f));
            Write((byte)(value.a * 255.0f));
        }

        public void WriteFull(RGBAColor value) {
            Write(value.r);
            Write(value.g);
            Write(value.b);
            Write(value.a);
        }

        public void Write(Vector3 vectorValue, Heading headingValue) {
            Write((byte)((vectorValue.Z + 1.0f) * 255.0f / 2.0f));
            Write((byte)((vectorValue.Y + 1.0f) * 255.0f / 2.0f));
            Write((byte)((vectorValue.X + 1.0f) * 255.0f / 2.0f));
            Write((byte)(headingValue.rotDegrees / 360.0f * 255.0f));
        }

        public void Write(CellId value) {
            Write(value.id);
        }

        public void Write(DataId value) {
            Write(value.id);
        }

        public void Write(QualifiedDataId value) {
            Write((uint)value.dbType);
            Write(value.did);
        }

        public void Write(EnumId value) {
            Write(value.id);
        }

        public void Align(uint bytes) {
            long alignDelta = BaseStream.Position % bytes;
            if (alignDelta != 0) {
                for (long i = 0; i < bytes - alignDelta; i++) {
                    Write((byte)0);
                }
            }
        }
    }
}
