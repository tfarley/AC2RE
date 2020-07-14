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
    }
}
