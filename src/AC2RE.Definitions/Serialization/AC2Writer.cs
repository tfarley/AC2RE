using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace AC2RE.Definitions;

public class AC2Writer : BinaryWriter {

    private static readonly byte UNINITIALIZED_DATA = 0xCD;

    public readonly HeapObjectRegistry heapObjectRegistry;

    public AC2Writer(Stream output) : base(output) {
        heapObjectRegistry = new();
    }

    public AC2Writer(Stream output, HeapObjectRegistry heapObjectRegistry) : base(output) {
        this.heapObjectRegistry = heapObjectRegistry;
    }

    public override void Write(string value) {
        Write(value, Encoding.ASCII);
    }

    public override void Write(bool value) {
        Write((uint)(value ? 1 : 0));
    }

    public void WriteCompressed(uint value) {
        // SB_As32Bit_Compressed::Serialize
        if (value <= 0x7F) {
            Write((byte)(value & 0xFF));
        } else if (value <= 0x3FFF) {
            Write((byte)(((value >> 8) & 0xFF) | 0x80));
            Write((byte)(value & 0xFF));
        } else {
            Write((byte)(((value >> 24) & 0xFF) | 0xC0));
            Write((byte)((value >> 16) & 0xFF));
            Write((ushort)(value & 0xFFFF));
        }
    }

    // NOTE: Special naming to avoid stomping on ushort overload
    private void WritePackTag(PackTag value) {
        Write((uint)value);
    }

    public void Pack(bool value) {
        Pack((uint)(value ? 1 : 0));
    }

    public void Pack(int value) {
        WritePackTag(PackTag.Int);
        Write(value);
    }

    public void Pack(uint value) {
        WritePackTag(PackTag.Int);
        Write(value);
    }

    public void Pack(long value) {
        WritePackTag(PackTag.LongInt);
        Write(value);
    }

    public void Pack(ulong value) {
        WritePackTag(PackTag.LongInt);
        Write(value);
    }

    public void Pack(float value) {
        WritePackTag(PackTag.Float);
        Write(value);
    }

    public void Pack(double value) {
        WritePackTag(PackTag.LongFloat);
        Write(value);
    }

    public void Pack(InstanceId value) {
        Pack(value.id);
    }

    public void Pack(DataId value) {
        Pack(value.id);
    }

    public void Pack(EffectId value) {
        Pack(value.id);
    }

    public void PackEnum<T>(T value) where T : struct, Enum {
        Pack(Unsafe.As<T, uint>(ref value));
    }

    public void Pack(IHeapObject value) {
        heapObjectRegistry.references.Add(value);

        MemoryStream buffer = new();
        using (AC2Writer data = new(buffer, heapObjectRegistry)) {
            for (int i = 0; i < heapObjectRegistry.references.Count; i++) {
                IHeapObject referencedHeapObject = heapObjectRegistry.references[i];
                if (referencedHeapObject != null) {
                    writeHeapObject(data, referencedHeapObject);
                }
            }
        }

        for (int i = heapObjectRegistry.references.Count - 1; i >= 0; i--) {
            if (heapObjectRegistry.references[i] == null) {
                heapObjectRegistry.references.RemoveAt(i);
            }
        }

        WritePackTag(PackTag.Package);
        Write(heapObjectRegistry.references, v => Write(heapObjectRegistry.getId(v)));
        Write(buffer.ToArray());

        heapObjectRegistry.references.Clear();
    }

    private void writeHeapObject(AC2Writer data, IHeapObject value) {
        ReferenceEntry referenceEntry = heapObjectRegistry.getMeta(value).referenceEntry;

        data.Write(referenceEntry.blob & 0xFFFFFF00);

        if (referenceEntry.referenceType == ReferenceType.HeapObject) {
            if (referenceEntry.isSingleton) {
                value.write(data);
            } else if (!referenceEntry.isFiller || referenceEntry.isRoot) {
                data.Write((uint)0);
                data.Write((ushort)value.nativeType);
                data.Write(value.nativeType != NativeType.Undef ? (ushort)0xFFFF : (ushort)value.packageType);

                if (value.nativeType != NativeType.Undef) {
                    value.write(data);
                }

                uint size = value.packageType != PackageType.Undef
                    ? InterpMeta.getSize(value.GetType())
                    : 0;
                data.Write(size);

                if (value.nativeType == NativeType.Undef) {
                    value.write(data);
                }

                // Write out tags
                if (size > 0) {
                    foreach (FieldDesc fieldDesc in InterpMeta.getFieldDescs(value.GetType())) {
                        data.Write((byte)fieldDesc.fieldType);
                        if (fieldDesc.numWords == 2) {
                            data.Write(UNINITIALIZED_DATA);
                        }
                    }
                }
                data.Align(4);
            } else {
                throw new InvalidDataException(referenceEntry.blob.ToString());
            }
        } else {
            throw new NotImplementedException(referenceEntry.referenceType.ToString());
        }
    }

    public void WriteHO<T>(T value) where T : IHeapObject {
        if (value != null) {
            Write(heapObjectRegistry.getId(value));
            heapObjectRegistry.references.Add(value);
        } else {
            Write(ReferenceId.NULL);
        }
    }

    public void WriteHOFullRef<T>(T value) where T : IHeapObject {
        if (value != null) {
            new ReferenceIdWrapper(heapObjectRegistry.getId(value)).write(this);
            heapObjectRegistry.references.Add(value);
        } else {
            new ReferenceIdWrapper(ReferenceId.NULL).write(this);
        }
    }

    public void Write(string str, Encoding encoding) {
        int numChars = str?.Length ?? 0;
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

    public void WriteMultiByteString(string str) {
        int numChars = str?.Length ?? 0;
        WriteCompressed((uint)numChars);
        if (numChars == 0) {
            return;
        }
        byte[] bytes = Encoding.Unicode.GetBytes(str);
        Write(bytes);
    }

    public void Write<T>(List<T> list, Action<T> elementWriter, uint sizeOfSize = 4) {
        int count = list?.Count ?? 0;
        if (sizeOfSize == 1) {
            Write((byte)count);
        } else if (sizeOfSize == 2) {
            Write((ushort)count);
        } else if (sizeOfSize == 4) {
            Write((uint)count);
        } else {
            throw new ArgumentException(sizeOfSize.ToString());
        }
        if (count > 0) {
            foreach (var element in list) {
                elementWriter.Invoke(element);
            }
        }
    }

    public void Write<T>(HashSet<T> set, Action<T> elementWriter) {
        int count = set?.Count ?? 0;
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
        int count = dict?.Count ?? 0;
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
        int count = dict?.Count ?? 0;
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
        int count = dict?.Count ?? 0;
        Write(count);
        if (count > 0) {
            foreach (var element in dict) {
                keyWriter.Invoke(element.Key);
                valueWriter.Invoke(element.Value);
            }
        }
    }

    public void Write(ReferenceId value) {
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

    public void Write(LocalCellId value) {
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

    public void Write(StringId value) {
        Write(value.id);
    }

    public void Write(EffectId value) {
        Write(value.id);
    }

    public void WriteEnum16<T>(T value) where T : struct, Enum {
        Write(Unsafe.As<T, ushort>(ref value));
    }

    public void WriteEnum<T>(T value) where T : struct, Enum {
        Write(Unsafe.As<T, uint>(ref value));
    }

    public void WriteEnum64<T>(T value) where T : struct, Enum {
        Write(Unsafe.As<T, ulong>(ref value));
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
