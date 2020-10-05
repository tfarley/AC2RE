using AC2E.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;

namespace AC2E.Def {

    public class AC2Reader : BinaryReader {

        public readonly PackageRegistry packageRegistry;

        public AC2Reader(Stream input) : base(input) {
            packageRegistry = new PackageRegistry();
        }

        public AC2Reader(Stream input, PackageRegistry packageRegistry) : base(input) {
            this.packageRegistry = packageRegistry;
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
            checkPackTag(PackTag.INT);
            return ReadInt32();
        }

        public uint UnpackUInt32() {
            checkPackTag(PackTag.INT);
            return ReadUInt32();
        }

        public long UnpackInt64() {
            checkPackTag(PackTag.LONGINT);
            return ReadInt64();
        }

        public ulong UnpackUInt64() {
            checkPackTag(PackTag.LONGINT);
            return ReadUInt64();
        }

        public float UnpackSingle() {
            checkPackTag(PackTag.FLOAT);
            return ReadSingle();
        }

        public double UnpackDouble() {
            checkPackTag(PackTag.LONGFLOAT);
            return ReadDouble();
        }

        public InstanceId UnpackInstanceId() {
            return new InstanceId(UnpackUInt64());
        }

        public DataId UnpackDataId() {
            return new DataId(UnpackUInt32());
        }

        public T UnpackPackage<T>(bool skipPackTag = false) where T : IPackage {
            if (!skipPackTag) {
                checkPackTag(PackTag.PACKAGE);
            }

            List<PackageId> packageIds = ReadList(ReadPackageId);

            T rootPackage = readPackage<T>(packageIds[0]);

            for (int i = 1; i < packageIds.Count; i++) {
                readPackage<IPackage>(packageIds[i]);
            }

            packageRegistry.executeResolvers();

            return rootPackage;
        }

        private T readPackage<T>(PackageId packageId) {
            InterpReferenceMeta referenceMeta = new InterpReferenceMeta(ReadUInt32());

            IPackage package;

            if (referenceMeta.isSingleton) {
                package = new SingletonPkg<IPackage> {
                    did = ReadDataId(),
                };
            } else {
                uint unk1 = ReadUInt32();
                NativeType nativeType = (NativeType)ReadUInt16();
                PackageType packageType = (PackageType)ReadUInt16();
                if (nativeType != NativeType.UNDEF) {
                    package = PackageManager.read(this, nativeType);
                } else {
                    uint packageLength = ReadUInt32() * 4;
                    if (packageLength == 0) {
                        package = new GenericPackage(packageType);
                    } else {
                        long startPos = BaseStream.Position;
                        package = PackageManager.read(this, packageType);
                        uint readLength = (uint)(BaseStream.Position - startPos);
                        if (readLength != packageLength) {
                            throw new InvalidDataException(((int)(readLength - packageLength)).ToString());
                        }
                    }
                }

                // TODO: Still not sure this is the correct condition for whether there are references or not
                // Skip over field descriptions
                if (nativeType == NativeType.UNDEF) {
                    foreach (FieldDesc fieldDesc in InterpMeta.getFieldDescs(package.GetType())) {
                        ReadByte();
                        if (fieldDesc.numWords == 2) {
                            ReadByte();
                        }
                    }
                    // TODO: Should this align occur even if there are no references?
                    Align(4);
                } else {
                    // TODO: Is this needed/correct?
                    ReadUInt32();
                }
            }

            packageRegistry.register(packageId, package, referenceMeta);

            return (T)package;
        }

        public PackageId ReadPkg<T>(Action<T> assigner) where T : IPackage {
            PackageId packageId = ReadPackageId();
            if (packageId != PackageId.NULL) {
                packageRegistry.addResolver(() => assigner.Invoke(packageRegistry.get<T>(packageId)));
            }
            return packageId;
        }

        public PackageId ReadSingletonPkg<T>(Action<SingletonPkg<T>> assigner) where T : class, IPackage {
            PackageId packageId = ReadPackageId();
            if (packageId != PackageId.NULL) {
                packageRegistry.addResolver(() => assigner.Invoke(SingletonPkg<T>.cast(packageRegistry.get<IPackage>(packageId))));
            }
            return packageId;
        }

        public string ReadNullTermString(Encoding encoding = null) {
            if (encoding == null) {
                encoding = Encoding.ASCII;
            }
            MemoryStream buffer = new MemoryStream();
            using (AC2Writer data = new AC2Writer(buffer, packageRegistry)) {
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
            List<T> list = new List<T>();
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
                throw new ArgumentException();
            }
            for (int i = 0; i < numElements; i++) {
                list.Add(elementReader.Invoke());
            }
        }

        public HashSet<T> ReadSet<T>(Func<T> elementReader) {
            HashSet<T> set = new HashSet<T>();
            ReadSet(set, elementReader);
            return set;
        }

        public void ReadSet<T>(HashSet<T> set, Func<T> elementReader) {
            ushort numElements = ReadUInt16();
            ushort setSize = ReadUInt16();
            for (int i = 0; i < numElements; i++) {
                set.Add(elementReader.Invoke());
            }
        }

        public Dictionary<K, List<V>> ReadMultiDictionary<K, V>(Func<K> keyReader, Func<V> valueReader) {
            Dictionary<K, List<V>> dict = new Dictionary<K, List<V>>();
            ReadMultiDictionary(dict, keyReader, valueReader);
            return dict;
        }

        public void ReadMultiDictionary<K, V>(Dictionary<K, List<V>> dict, Func<K> keyReader, Func<V> valueReader) {
            uint numElements = ReadUInt32();
            for (int i = 0; i < numElements; i++) {
                dict.GetOrCreate(keyReader.Invoke()).Add(valueReader.Invoke());
            }
        }

        public Dictionary<K, V> ReadDictionary<K, V>(Func<K> keyReader, Func<V> valueReader) {
            Dictionary<K, V> dict = new Dictionary<K, V>();
            ReadDictionary(dict, keyReader, valueReader);
            return dict;
        }

        public void ReadDictionary<K, V>(Dictionary<K, V> dict, Func<K> keyReader, Func<V> valueReader) {
            ushort numElements = ReadUInt16();
            ushort tableSize = ReadUInt16();
            for (int i = 0; i < numElements; i++) {
                dict.Add(keyReader.Invoke(), valueReader.Invoke());
            }
        }

        public Dictionary<K, V> ReadStlMap<K, V>(Func<K> keyReader, Func<V> valueReader) {
            Dictionary<K, V> dict = new Dictionary<K, V>();
            ReadStlMap(dict, keyReader, valueReader);
            return dict;
        }

        public void ReadStlMap<K, V>(Dictionary<K, V> dict, Func<K> keyReader, Func<V> valueReader) {
            // Variation of dictionary where the count is a full 32 bits without any table size (used for std::map specifically, see STREAMPACK_STL)
            uint numElements = ReadUInt32();
            for (int i = 0; i < numElements; i++) {
                dict.Add(keyReader.Invoke(), valueReader.Invoke());
            }
        }

        public PackageId ReadPackageId() {
            return new PackageId(ReadUInt32());
        }

        public PackageId ReadPackageFullRef() {
            return new ReferenceId(this).id;
        }

        public InstanceId ReadInstanceId() {
            return new InstanceId(ReadUInt64());
        }

        public InstanceIdWithStamp ReadInstanceIdWithStamp() {
            return new InstanceIdWithStamp {
                id = ReadInstanceId(),
                instanceStamp = ReadUInt16(),
                otherStamp = ReadUInt16(),
            };
        }

        public Vector3 ReadVector() {
            return new Vector3(
                ReadSingle(),
                ReadSingle(),
                ReadSingle()
                );
        }

        public Quaternion ReadQuaternion() {
            float w = ReadSingle();
            return new Quaternion(
                ReadSingle(),
                ReadSingle(),
                ReadSingle(),
                w
                );
        }

        public Matrix4x4 ReadMatrix4x4() {
            return new Matrix4x4(
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
            return new RGBAColor {
                r = ReadByte() / 255.0f,
                g = ReadByte() / 255.0f,
                b = ReadByte() / 255.0f,
                a = ReadByte() / 255.0f,
            };
        }

        public RGBAColor ReadRGBAColorFull() {
            return new RGBAColor {
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
            return new Tuple<Vector3, Heading>(new Vector3(x, y, z), new Heading(ReadByte() / 255.0f * 360.0f));
        }

        public CellId ReadCellId() {
            return new CellId(ReadUInt32());
        }

        public DataId ReadDataId() {
            return new DataId(ReadUInt32());
        }

        public QualifiedDataId ReadQualifiedDataId() {
            return new QualifiedDataId((DbType)ReadUInt32(), ReadDataId());
        }

        public void Align(uint bytes) {
            long alignDelta = BaseStream.Position % bytes;
            if (alignDelta != 0) {
                BaseStream.Position += bytes - alignDelta;
            }
        }
    }
}
