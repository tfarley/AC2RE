﻿namespace AC2RE.Definitions {

    public class FactionGlobals : IPackage {

        public PackageType packageType => PackageType.FactionGlobals;

        public uint pad1;

        public FactionGlobals(AC2Reader data) {
            pad1 = data.ReadUInt32();
        }
    }
}
