namespace AC2E.Def {

    public class FellowVitals : IPackage {

        public PackageType packageType => PackageType.FellowVitals;

        public uint maxHealth; // m_max_health
        public CellId cell; // m_cell
        public uint health; // m_health
        public uint pkDamage; // m_PKDamage
        public uint maxVigor; // m_max_vigor
        public uint vigor; // m_vigor
        public uint pkVigorloss; // m_PKVigorloss

        public FellowVitals() {

        }

        public FellowVitals(AC2Reader data) {
            maxHealth = data.ReadUInt32();
            cell = data.ReadCellId();
            health = data.ReadUInt32();
            pkDamage = data.ReadUInt32();
            maxVigor = data.ReadUInt32();
            vigor = data.ReadUInt32();
            pkVigorloss = data.ReadUInt32();
        }

        public void write(AC2Writer data) {
            data.Write(maxHealth);
            data.Write(cell);
            data.Write(health);
            data.Write(pkDamage);
            data.Write(maxVigor);
            data.Write(vigor);
            data.Write(pkVigorloss);
        }
    }
}
