namespace AC2E.Def {

    public class FellowVitals : IPackage {

        public PackageType packageType => PackageType.FellowVitals;

        public uint m_max_health;
        public CellId m_cell;
        public uint m_health;
        public uint m_PKDamage;
        public uint m_max_vigor;
        public uint m_vigor;
        public uint m_PKVigorloss;

        public FellowVitals() {

        }

        public FellowVitals(AC2Reader data) {
            m_max_health = data.ReadUInt32();
            m_cell = data.ReadCellId();
            m_health = data.ReadUInt32();
            m_PKDamage = data.ReadUInt32();
            m_max_vigor = data.ReadUInt32();
            m_vigor = data.ReadUInt32();
            m_PKVigorloss = data.ReadUInt32();
        }

        public void write(AC2Writer data) {
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
