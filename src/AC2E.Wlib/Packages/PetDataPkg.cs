﻿using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class PetDataPkg : IPackage {

        public PackageType packageType => PackageType.PetData;

        public double m_timeLeftWorld;
        public uint m_class;
        public uint m_flags;

        public PetDataPkg() {

        }

        public PetDataPkg(BinaryReader data) {
            m_timeLeftWorld = data.ReadDouble();
            m_class = data.ReadUInt32();
            m_flags = data.ReadUInt32();
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(m_timeLeftWorld);
            data.Write(m_class);
            data.Write(m_flags);
        }
    }
}
