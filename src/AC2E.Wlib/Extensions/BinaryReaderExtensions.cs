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

            List<PackageId> references = reader.ReadList(() => new PackageId(reader.ReadUInt32()));

            T package = readPackage<T>(reader);

            List<IPackage> referencedPackages = new List<IPackage>();
            for (int i = 1; i < references.Count; i++) {
                referencedPackages.Add(readPackage<IPackage>(reader));
            }

            // TODO: Referenced packages should be stored in some global registry Dictionary<PackageId, IPackage> replacing the entry if needed, so that they can be looked up when needed

            return package;
        }

        private static T readPackage<T>(BinaryReader reader) {
            InterpReferenceMeta referenceMeta = new InterpReferenceMeta(reader.ReadUInt32());

            // TODO: How to read this if type is not serialized? Is it always just a single DataId, like for EffectPkg (in which case a GenericSingletonPkg with a set-able type member might be better)?
            if (referenceMeta.isSingleton) {
                DataId did = reader.ReadDataId();
                throw new NotImplementedException($"Not sure about data reading for package, possible DataId: {did}.");
            }

            IPackage package;

            uint unk1 = reader.ReadUInt32();
            NativeType nativeType = (NativeType)reader.ReadUInt16();
            PackageType packageType = (PackageType)reader.ReadUInt16();
            if (nativeType != NativeType.UNDEF) {
                package = NativePackageFactory.read(nativeType, reader);
            } else {
                uint length = reader.ReadUInt32();
                package = PackageFactory.read(packageType, reader);
            }

            // Skip over field descriptions - this will also handle all the UNINITIALIZED_DATA for multi-word types too
            while (reader.ReadByte() != 0) ;
            reader.Align(4);

            return (T)package;
        }
    }
}
