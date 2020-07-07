﻿using AC2E.Def;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public class AAHashPkg : IPackage {

        public NativeType nativeType => NativeType.AAHASH;
        public PackageType packageType => PackageType.UNDEF;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAPOBJECT);

        public PackageId id { get; set; }

        public Dictionary<uint, uint> contents;

        public AAHashPkg() {

        }

        public AAHashPkg(BinaryReader data) {
            contents = data.ReadDictionary(data.ReadUInt32, data.ReadUInt32);
        }

        public void write(BinaryWriter data, List<IPackage> references) {
            data.Write(contents, data.Write, data.Write);
        }
    }
}
