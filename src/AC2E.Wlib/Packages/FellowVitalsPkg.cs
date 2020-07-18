﻿using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class FellowVitalsPkg : IPackage {

        public PackageType packageType => PackageType.FellowVitals;

        public uint m_max_health;
        public CellId m_cell;
        public uint m_health;
        public uint m_PKDamage;
        public uint m_max_vigor;
        public uint m_vigor;
        public uint m_PKVigorloss;

        public FellowVitalsPkg() {

        }

        public FellowVitalsPkg(BinaryReader data, PackageRegistry registry) {
            m_max_health = data.ReadUInt32();
            m_cell = data.ReadCellId();
            m_health = data.ReadUInt32();
            m_PKDamage = data.ReadUInt32();
            m_max_vigor = data.ReadUInt32();
            m_vigor = data.ReadUInt32();
            m_PKVigorloss = data.ReadUInt32();
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(m_max_health);
            data.Write(m_cell);
            data.Write(m_health);
            data.Write(m_PKDamage);
            data.Write(m_max_vigor);
            data.Write(m_vigor);
            data.Write(m_PKVigorloss);
        }
    }
}
