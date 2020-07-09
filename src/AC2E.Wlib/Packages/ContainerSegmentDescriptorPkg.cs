﻿using AC2E.Def;
using AC2E.Interp;
using System.Collections.Generic;
using System.IO;

namespace AC2E.WLib {

    public class ContainerSegmentDescriptorPkg : IPackage {

        public NativeType nativeType => NativeType.UNDEF;
        public PackageType packageType => PackageType.ContainerSegmentDescriptor;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAPOBJECT);

        public PackageId id { get; set; }

        public uint mSegmentMaxSize;
        public uint mSegmentSize;

        public ContainerSegmentDescriptorPkg() {

        }

        public ContainerSegmentDescriptorPkg(BinaryReader data) {
            mSegmentMaxSize = data.ReadUInt32();
            mSegmentSize = data.ReadUInt32();
        }

        public void write(BinaryWriter data, List<PkgRef<IPackage>> references) {
            data.Write(mSegmentMaxSize);
            data.Write(mSegmentSize);
        }
    }
}
