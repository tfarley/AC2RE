using AC2E.Dat;
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

            List<PackageId> references = reader.ReadList(reader.ReadPackageId);

            T package = readPackage<T>(references[0], reader);
            PackageManager.add(package);

            List<IPackage> referencedPackages = new List<IPackage>();
            for (int i = 1; i < references.Count; i++) {
                IPackage referencedPackage = readPackage<IPackage>(references[i], reader);
                referencedPackages.Add(referencedPackage);
                PackageManager.add(referencedPackage);
            }

            package.resolveGenericRefs();
            foreach (IPackage referencedPackage in referencedPackages) {
                referencedPackage.resolveGenericRefs();
            }

            return package;
        }

        private static T readPackage<T>(PackageId packageId, BinaryReader reader) {
            InterpReferenceMeta referenceMeta = new InterpReferenceMeta(reader.ReadUInt32());

            if (referenceMeta.isSingleton) {
                return (T)(IPackage)new SingletonPkg {
                    did = reader.ReadDataId(),
                };
            }

            IPackage package;

            uint unk1 = reader.ReadUInt32();
            NativeType nativeType = (NativeType)reader.ReadUInt16();
            PackageType packageType = (PackageType)reader.ReadUInt16();
            if (nativeType != NativeType.UNDEF) {
                package = PackageManager.read(packageId, nativeType, reader);
            } else {
                uint length = reader.ReadUInt32();
                package = PackageManager.read(packageId, packageType, reader);
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

            return (T)package;
        }

        public static PkgRef<T> ReadPkgRef<T>(this BinaryReader reader) where T : IPackage {
            return new PkgRef<T>(reader.ReadPackageId());
        }
    }
}
