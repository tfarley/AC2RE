using AC2RE.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;

namespace AC2RE.Definitions;

public class AC2Reader : BinaryReader {

    public readonly HeapObjectRegistry heapObjectRegistry;

    public AC2Reader(Stream input) : base(input) {
        heapObjectRegistry = new();
    }

    public AC2Reader(Stream input, HeapObjectRegistry heapObjectRegistry) : base(input) {
        this.heapObjectRegistry = heapObjectRegistry;
    }

    public override string ReadString() {
        return ReadString(Encoding.ASCII);
    }

    public override bool ReadBoolean() {
        return ReadUInt32() != 0;
    }

    private PackTag ReadPackTag() {
        return (PackTag)ReadUInt32();
    }

    private void checkPackTag(PackTag expectedPackTag) {
        PackTag packTag = ReadPackTag();
        if (packTag != expectedPackTag) {
            throw new InvalidDataException(packTag.ToString());
        }
    }

    public bool UnpackBoolean() {
        return UnpackUInt32() != 0;
    }

    public int UnpackInt32() {
        checkPackTag(PackTag.Int);
        return ReadInt32();
    }

    public uint UnpackUInt32() {
        checkPackTag(PackTag.Int);
        return ReadUInt32();
    }

    public long UnpackInt64() {
        checkPackTag(PackTag.LongInt);
        return ReadInt64();
    }

    public ulong UnpackUInt64() {
        checkPackTag(PackTag.LongInt);
        return ReadUInt64();
    }

    public float UnpackSingle() {
        checkPackTag(PackTag.Float);
        return ReadSingle();
    }

    public double UnpackDouble() {
        checkPackTag(PackTag.LongFloat);
        return ReadDouble();
    }

    public InstanceId UnpackInstanceId() {
        return new(UnpackUInt64());
    }

    public DataId UnpackDataId() {
        return new(UnpackUInt32());
    }

    public EffectId UnpackEffectId() {
        return new(UnpackUInt32());
    }

    public T UnpackHeapObject<T>(bool skipPackTag = false) where T : IHeapObject {
        if (!skipPackTag) {
            checkPackTag(PackTag.Package);
        }

        List<ReferenceId> referenceIds = ReadList(ReadReferenceId);

        T rootHeapObject = readHeapObject<T>(referenceIds[0]);

        for (int i = 1; i < referenceIds.Count; i++) {
            readHeapObject<IHeapObject>(referenceIds[i]);
        }

        heapObjectRegistry.executeResolvers();

        return rootHeapObject;
    }

    private T readHeapObject<T>(ReferenceId referenceId) {
        ReferenceEntry referenceEntry = new(ReadUInt32());

        IHeapObject heapObject = null;

        if (referenceEntry.referenceType != ReferenceType.HeapObject) {
            throw new NotImplementedException(referenceEntry.referenceType.ToString());
        } else if (referenceEntry.isSingleton) {
            heapObject = new SingletonPkg<IHeapObject> {
                wstateDid = ReadDataId(),
            };
        } else if (!referenceEntry.isFiller || referenceEntry.isRoot) {
            uint stateId = ReadUInt32(); // m_pkgStateID
            NativeType nativeType = (NativeType)ReadUInt16();
            PackageType packageType = (PackageType)ReadUInt16();

            if (nativeType != NativeType.Undef) {
                heapObject = HeapObjectManager.read(this, nativeType);
            }

            uint size = ReadUInt32();

            if (nativeType == NativeType.Undef) {
                if (size == 0) {
                    heapObject = new GenericHeapObject(packageType);
                } else {
                    long startPos = BaseStream.Position;
                    heapObject = HeapObjectManager.read(this, packageType);
                    uint readLength = (uint)(BaseStream.Position - startPos);
                    uint byteSize = size * sizeof(uint);
                    if (readLength != byteSize) {
                        throw new InvalidDataException(((int)(readLength - byteSize)).ToString());
                    }
                }
            }

            // Skip over tags
            ReadBytes((int)size);
            Align(4);
        } else {
            throw new InvalidDataException(referenceEntry.blob.ToString());
        }

        heapObjectRegistry.register(referenceId, heapObject, referenceEntry);

        return (T)heapObject;
    }

    public ReferenceId ReadHO<T>(Action<T> assigner) where T : IHeapObject {
        ReferenceId referenceId = ReadReferenceId();
        if (referenceId != ReferenceId.NULL) {
            heapObjectRegistry.addResolver(() => assigner.Invoke(heapObjectRegistry.get<T>(referenceId)));
        }
        return referenceId;
    }

    public ReferenceId ReadHO<T>(Action<SingletonPkg<T>> assigner) where T : class, IHeapObject {
        ReferenceId referenceId = ReadReferenceId();
        if (referenceId != ReferenceId.NULL) {
            heapObjectRegistry.addResolver(() => assigner.Invoke(SingletonPkg<T>.cast(heapObjectRegistry.get<IHeapObject>(referenceId))));
        }
        return referenceId;
    }

    public string ReadNullTermString(Encoding encoding = null) {
        encoding ??= Encoding.ASCII;
        MemoryStream buffer = new();
        using (AC2Writer data = new(buffer, heapObjectRegistry)) {
            byte val;
            do {
                val = ReadByte();
                data.Write(val);
            } while (val != 0);
        }
        byte[] bytes = buffer.ToArray();
        return encoding.GetString(bytes, 0, bytes.Length - 1);
    }

    public string ReadString(Encoding encoding) {
        ushort numChars = ReadUInt16();
        if (numChars == 0) {
            Align(4);
            return "";
        }
        int length = numChars * encoding.GetMaxByteCount(0);
        byte[] bytes = ReadBytes(length);
        Align(4);
        AC2Crypto.decrypt(bytes, 0, length);
        return encoding.GetString(bytes);
    }

    public List<T> ReadList<T>(Func<T> elementReader, uint sizeOfSize = 4) {
        List<T> list = new();
        ReadList(list, elementReader, sizeOfSize);
        return list;
    }

    public void ReadList<T>(List<T> list, Func<T> elementReader, uint sizeOfSize = 4) {
        uint numElements;
        if (sizeOfSize == 1) {
            numElements = ReadByte();
        } else if (sizeOfSize == 2) {
            numElements = ReadUInt16();
        } else if (sizeOfSize == 4) {
            numElements = ReadUInt32();
        } else {
            throw new ArgumentException(sizeOfSize.ToString());
        }
        list.Capacity += (int)numElements;
        for (int i = 0; i < numElements; i++) {
            list.Add(elementReader.Invoke());
        }
    }

    public HashSet<T> ReadSet<T>(Func<T> elementReader) {
        HashSet<T> set = new();
        ReadSet(set, elementReader);
        return set;
    }

    public void ReadSet<T>(HashSet<T> set, Func<T> elementReader) {
        ushort numElements = ReadUInt16();
        ushort setSize = ReadUInt16();
        set.EnsureCapacity(set.Count + numElements);
        for (int i = 0; i < numElements; i++) {
            set.Add(elementReader.Invoke());
        }
    }

    public Dictionary<K, List<V>> ReadMultiDictionary<K, V>(Func<K> keyReader, Func<V> valueReader) {
        Dictionary<K, List<V>> dict = new();
        ReadMultiDictionary(dict, keyReader, valueReader);
        return dict;
    }

    public void ReadMultiDictionary<K, V>(Dictionary<K, List<V>> dict, Func<K> keyReader, Func<V> valueReader) {
        uint numElements = ReadUInt32();
        dict.EnsureCapacity(dict.Count + (int)numElements);
        for (int i = 0; i < numElements; i++) {
            dict.GetOrCreate(keyReader.Invoke()).Add(valueReader.Invoke());
        }
    }

    public Dictionary<K, V> ReadDictionary<K, V>(Func<K> keyReader, Func<V> valueReader) {
        Dictionary<K, V> dict = new();
        ReadDictionary(dict, keyReader, valueReader);
        return dict;
    }

    public void ReadDictionary<K, V>(Dictionary<K, V> dict, Func<K> keyReader, Func<V> valueReader) {
        ushort numElements = ReadUInt16();
        ushort tableSize = ReadUInt16();
        dict.EnsureCapacity(dict.Count + numElements);
        for (int i = 0; i < numElements; i++) {
            dict.Add(keyReader.Invoke(), valueReader.Invoke());
        }
    }

    public Dictionary<K, V> ReadStlMap<K, V>(Func<K> keyReader, Func<V> valueReader) {
        Dictionary<K, V> dict = new();
        ReadStlMap(dict, keyReader, valueReader);
        return dict;
    }

    public void ReadStlMap<K, V>(Dictionary<K, V> dict, Func<K> keyReader, Func<V> valueReader) {
        // Variation of dictionary where the count is a full 32 bits without any table size (used for std::map specifically, see STREAMPACK_STL)
        uint numElements = ReadUInt32();
        dict.EnsureCapacity(dict.Count + (int)numElements);
        for (int i = 0; i < numElements; i++) {
            dict.Add(keyReader.Invoke(), valueReader.Invoke());
        }
    }

    public ReferenceId ReadReferenceId() {
        return new(ReadUInt32());
    }

    public ReferenceId ReadHOFullRef() {
        return new ReferenceIdWrapper(this).id;
    }

    public InstanceId ReadInstanceId() {
        return new(ReadUInt64());
    }

    public InstanceIdWithStamp ReadInstanceIdWithStamp() {
        return new() {
            id = ReadInstanceId(),
            instanceStamp = ReadUInt16(),
            otherStamp = ReadUInt16(),
        };
    }

    public Vector3 ReadVector() {
        return new(
            ReadSingle(),
            ReadSingle(),
            ReadSingle()
            );
    }

    public Quaternion ReadQuaternion() {
        float w = ReadSingle();
        return new(
            ReadSingle(),
            ReadSingle(),
            ReadSingle(),
            w
            );
    }

    public Matrix4x4 ReadMatrix4x4() {
        return new(
            ReadSingle(),
            ReadSingle(),
            ReadSingle(),
            ReadSingle(),
            ReadSingle(),
            ReadSingle(),
            ReadSingle(),
            ReadSingle(),
            ReadSingle(),
            ReadSingle(),
            ReadSingle(),
            ReadSingle(),
            ReadSingle(),
            ReadSingle(),
            ReadSingle(),
            ReadSingle()
            );
    }

    public RGBAColor ReadRGBAColor() {
        return new() {
            r = ReadByte() / 255.0f,
            g = ReadByte() / 255.0f,
            b = ReadByte() / 255.0f,
            a = ReadByte() / 255.0f,
        };
    }

    public RGBAColor ReadRGBAColorFull() {
        return new() {
            r = ReadSingle(),
            g = ReadSingle(),
            b = ReadSingle(),
            a = ReadSingle(),
        };
    }

    public Tuple<Vector3, Heading> ReadVectorHeadingPack() {
        float z = ReadByte() / 255.0f * 2.0f - 1.0f;
        float y = ReadByte() / 255.0f * 2.0f - 1.0f;
        float x = ReadByte() / 255.0f * 2.0f - 1.0f;
        return new(new(x, y, z), new(ReadByte() / 255.0f * 360.0f));
    }

    public CellId ReadCellId() {
        return new(ReadUInt32());
    }

    public LocalCellId ReadLocalCellId() {
        return new(ReadUInt16());
    }

    public DataId ReadDataId() {
        return new(ReadUInt32());
    }

    public QualifiedDataId ReadQualifiedDataId() {
        return new((DbType)ReadUInt32(), ReadDataId());
    }

    public EnumId ReadEnumId() {
        return new(ReadUInt32());
    }

    public EffectId ReadEffectId() {
        return new(ReadUInt32());
    }

    public void Align(uint bytes) {
        long alignDelta = BaseStream.Position % bytes;
        if (alignDelta != 0) {
            BaseStream.Position += bytes - alignDelta;
        }
    }
}
