using AC2E.Def;
using AC2E.Interp;
using AC2E.Utils;
using System.Collections.Generic;
using System.IO;

namespace AC2E {

    public static class BinaryReaderExtensions {

        public static T UnpackPackage<T>(this BinaryReader reader) where T : IPackage {
            if ((PackTag)reader.ReadUInt32() != PackTag.PACKAGE) {
                throw new InvalidDataException();
            }

            List<PackageId> packageIds = reader.ReadList(reader.ReadPackageId);
            IPackage[] allPackages = new IPackage[packageIds.Count];

            T rootPackage = readPackage<T>(packageIds[0], reader);
            allPackages[0] = rootPackage;

            for (int i = 1; i < packageIds.Count; i++) {
                IPackage referencedPackage = readPackage<IPackage>(packageIds[i], reader);
                allPackages[i] = referencedPackage;
            }

            foreach (IPackage package in allPackages) {
                package.resolveRefs();
            }

            return rootPackage;
        }

        private static T readPackage<T>(PackageId packageId, BinaryReader reader) {
            InterpReferenceMeta referenceMeta = new InterpReferenceMeta(reader.ReadUInt32());

            IPackage package;

            if (referenceMeta.isSingleton) {
                package = new SingletonPkg {
                    did = reader.ReadDataId(),
                };

                PackageManager.register(packageId, package, referenceMeta);

                return (T)package;
            }

            uint unk1 = reader.ReadUInt32();
            NativeType nativeType = (NativeType)reader.ReadUInt16();
            PackageType packageType = (PackageType)reader.ReadUInt16();
            if (nativeType != NativeType.UNDEF) {
                package = PackageManager.read(nativeType, reader);
            } else {
                uint length = reader.ReadUInt32();
                package = PackageManager.read(packageType, reader);
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

            PackageManager.register(packageId, package, referenceMeta);

            return (T)package;
        }

        public static PkgRef<T> ReadPkgRef<T>(this BinaryReader reader) where T : IPackage {
            return new PkgRef<T>(reader.ReadPackageId());
        }
    }
}
