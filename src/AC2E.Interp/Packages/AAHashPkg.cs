﻿using AC2E.Def.Extensions;
using AC2E.Interp.Extensions;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp.Packages {

    public class AAHashPkg : IPackage {

        public NativeType nativeType => NativeType.AAHASH;
        public PackageType packageType => PackageType.UNDEF;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAP_OBJECT);

        public uint id { get; set; }

        public Dictionary<uint, uint> contents;

        public void write(BinaryWriter data, List<IPackage> references) {
            data.Write(contents, data.Write, data.Write);
        }
    }
}
