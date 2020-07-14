﻿using AC2E.Interp;
using System.Collections.Generic;
using System.IO;

namespace AC2E.WLib {

    public class ContainerSegmentDescriptorPkg : IPackage {

        public PackageType packageType => PackageType.ContainerSegmentDescriptor;

        public uint mSegmentMaxSize;
        public uint mSegmentSize;

        public ContainerSegmentDescriptorPkg() {

        }

        public ContainerSegmentDescriptorPkg(BinaryReader data) {
            mSegmentMaxSize = data.ReadUInt32();
            mSegmentSize = data.ReadUInt32();
        }

        public void write(BinaryWriter data, List<IPackage> references) {
            data.Write(mSegmentMaxSize);
            data.Write(mSegmentSize);
        }
    }
}
