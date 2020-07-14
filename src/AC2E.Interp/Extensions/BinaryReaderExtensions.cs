using AC2E.Def;
using AC2E.Interp;
using AC2E.Utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace AC2E {

    public static class BinaryReaderExtensions {

        public static T UnpackPackage<T>(this BinaryReader reader) where T : IPackage {
            if ((PackTag)reader.ReadUInt32() != PackTag.PACKAGE) {
                throw new InvalidDataException();
            }

            PackageRegistry tempRegistry = new PackageRegistry();
            List<Action<PackageRegistry>> resolvers = new List<Action<PackageRegistry>>();

            List<PackageId> packageIds = reader.ReadList(reader.ReadPackageId);

            T rootPackage = readPackage<T>(tempRegistry, packageIds[0], reader, resolvers);

            for (int i = 1; i < packageIds.Count; i++) {
                readPackage<IPackage>(tempRegistry, packageIds[i], reader, resolvers);
            }

            // Reverse resolvers to ensure we convert the nested packages before the parents
            resolvers.Reverse();
            foreach (Action<PackageRegistry> resolver in resolvers) {
                resolver.Invoke(tempRegistry);
            }

            return rootPackage;
        }

        private static T readPackage<T>(PackageRegistry registry, PackageId packageId, BinaryReader reader, List<Action<PackageRegistry>> resolvers) {
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
                package = PackageManager.read(nativeType, reader, resolvers);
            } else {
                uint length = reader.ReadUInt32();
                package = PackageManager.read(packageType, reader, resolvers);
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

        public static PackageId ReadPkgRef<T>(this BinaryReader reader, Action<T> assigner, List<Action<PackageRegistry>> resolvers) where T : IPackage {
            PackageId packageId = reader.ReadPackageId();
            if (packageId.id != PackageId.NULL.id) {
                resolvers.Add(registry => assigner.Invoke(registry.get<T>(packageId)));
            }
            return packageId;
        }
    }
}
