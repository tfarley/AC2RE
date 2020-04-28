﻿using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp.Packages {

    public class AAMultiHashPkg : IPackage {

        public NativeType nativeType => NativeType.AAMULTIHASH;
        public PackageType packageType => PackageType.UNDEF;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAP_OBJECT);

        public uint id { get; set; }

        public Dictionary<uint, List<uint>> contents;

        public void write(BinaryWriter data, List<IPackage> references) {
            // TODO: Write correct format
        }
    }
}
