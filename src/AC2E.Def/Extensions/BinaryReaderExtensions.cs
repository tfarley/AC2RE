using AC2E.Def;
using AC2E.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AC2E {

    public static class BinaryReaderExtensions {

        public static int UnpackInt32(this BinaryReader reader) {
            PackTag packTag = (PackTag)reader.ReadUInt32();
            if (packTag != PackTag.INT) {
                throw new InvalidDataException(packTag.ToString());
            }

            return reader.ReadInt32();
        }

        public static uint UnpackUInt32(this BinaryReader reader) {
            PackTag packTag = (PackTag)reader.ReadUInt32();
            if (packTag != PackTag.INT) {
                throw new InvalidDataException(packTag.ToString());
            }

            return reader.ReadUInt32();
        }

        public static long UnpackInt64(this BinaryReader reader) {
            PackTag packTag = (PackTag)reader.ReadUInt32();
            if (packTag != PackTag.LONGINT) {
                throw new InvalidDataException(packTag.ToString());
            }

            return reader.ReadInt64();
        }

        public static ulong UnpackUInt64(this BinaryReader reader) {
            PackTag packTag = (PackTag)reader.ReadUInt32();
            if (packTag != PackTag.LONGINT) {
                throw new InvalidDataException(packTag.ToString());
            }

            return reader.ReadUInt64();
        }

        public static float UnpackSingle(this BinaryReader reader) {
            PackTag packTag = (PackTag)reader.ReadUInt32();
            if (packTag != PackTag.FLOAT) {
                throw new InvalidDataException(packTag.ToString());
            }

            return reader.ReadSingle();
        }

        public static double UnpackDouble(this BinaryReader reader) {
            PackTag packTag = (PackTag)reader.ReadUInt32();
            if (packTag != PackTag.LONGFLOAT) {
                throw new InvalidDataException(packTag.ToString());
            }

            return reader.ReadDouble();
        }

        public static InstanceId UnpackInstanceId(this BinaryReader reader) {
            PackTag packTag = (PackTag)reader.ReadUInt32();
            if (packTag != PackTag.LONGINT) {
                throw new InvalidDataException(packTag.ToString());
            }

            return new InstanceId(reader.ReadUInt64());
        }

        public static DataId UnpackDataId(this BinaryReader reader) {
            PackTag packTag = (PackTag)reader.ReadUInt32();
            if (packTag != PackTag.INT) {
                throw new InvalidDataException(packTag.ToString());
            }

            return new DataId(reader.ReadUInt32());
        }

        public static T UnpackPackage<T>(this BinaryReader reader) where T : IPackage {
            PackTag packTag = (PackTag)reader.ReadUInt32();
            if (packTag != PackTag.PACKAGE) {
                throw new InvalidDataException(packTag.ToString());
            }

            PackageRegistry tempRegistry = new PackageRegistry();

            List<PackageId> packageIds = reader.ReadList(reader.ReadPackageId);

            T rootPackage = readPackage<T>(reader, tempRegistry, packageIds[0]);

            for (int i = 1; i < packageIds.Count; i++) {
                readPackage<IPackage>(reader, tempRegistry, packageIds[i]);
            }

            tempRegistry.executeResolvers();

            return rootPackage;
        }

        private static T readPackage<T>(BinaryReader reader, PackageRegistry registry, PackageId packageId) {
            InterpReferenceMeta referenceMeta = new InterpReferenceMeta(reader.ReadUInt32());

            IPackage package;

            if (referenceMeta.isSingleton) {
                package = new SingletonPkg {
                    did = reader.ReadDataId(),
                };

                registry.register(packageId, package, referenceMeta);

                return (T)package;
            }

            uint unk1 = reader.ReadUInt32();
            NativeType nativeType = (NativeType)reader.ReadUInt16();
            PackageType packageType = (PackageType)reader.ReadUInt16();
            if (nativeType != NativeType.UNDEF) {
                package = PackageManager.read(reader, nativeType, registry);
            } else {
                uint length = reader.ReadUInt32();
                package = PackageManager.read(reader, packageType, registry);
            }

            // TODO: Still not sure this is the correct condition for whether there are references or not
            // Skip over field descriptions
            if (nativeType == NativeType.UNDEF) {
                foreach (FieldDesc fieldDesc in InterpMeta.getFieldDescs(package.GetType())) {
                    reader.ReadByte();
                    if (fieldDesc.numWords == 2) {
                        reader.ReadByte();
                    }
                }
                // TODO: Should this align occur even if there are no references?
                reader.Align(4);
            } else {
                // TODO: Is this needed/correct?
                reader.ReadUInt32();
            }

            registry.register(packageId, package, referenceMeta);

            return (T)package;
        }

        public static PackageId ReadPkgRef<T>(this BinaryReader reader, Action<T> assigner, PackageRegistry registry) where T : IPackage {
            PackageId packageId = reader.ReadPackageId();
            if (packageId.id != PackageId.NULL.id) {
                registry.addResolver(() => assigner.Invoke(registry.get<T>(packageId)));
            }
            return packageId;
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

        public static HashSet<T> ReadSet<T>(this BinaryReader reader, Func<T> elementReader) {
            HashSet<T> set = new HashSet<T>();
            ushort numElements = reader.ReadUInt16();
            ushort setSize = reader.ReadUInt16();
            for (int i = 0; i < numElements; i++) {
                set.Add(elementReader.Invoke());
            }
            return set;
        }

        public static Dictionary<K, List<V>> ReadMultiDictionary<K, V>(this BinaryReader reader, Func<K> keyReader, Func<V> valueReader) {
            Dictionary<K, List<V>> dict = new Dictionary<K, List<V>>();
            uint numElements = reader.ReadUInt32();
            for (int i = 0; i < numElements; i++) {
                dict.GetOrCreate(keyReader.Invoke()).Add(valueReader.Invoke());
            }
            return dict;
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

        public static PackageId ReadPackageId(this BinaryReader reader) {
            return new PackageId(reader.ReadUInt32());
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
                r = reader.ReadByte() / 255.0f,
                g = reader.ReadByte() / 255.0f,
                b = reader.ReadByte() / 255.0f,
                a = reader.ReadByte() / 255.0f,
            };
        }

        public static Heading ReadHeading(this BinaryReader reader) {
            return new Heading(((reader.ReadUInt32() >> 24) & 0x000000FF) / 255.0f * 360.0f);
        }

        public static CellId ReadCellId(this BinaryReader reader) {
            return new CellId(reader.ReadUInt32());
        }

        public static DataId ReadDataId(this BinaryReader reader) {
            return new DataId(reader.ReadUInt32());
        }

        public static QualifiedDataId ReadQualifiedDataId(this BinaryReader reader) {
            return new QualifiedDataId((DbType)reader.ReadUInt32(), reader.ReadDataId());
        }
    }
}
