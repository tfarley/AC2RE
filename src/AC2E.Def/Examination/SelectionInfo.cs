namespace AC2E.Def {

    public class SelectionInfo : IPackage {

        public NativeType nativeType => NativeType.SELECTIONINFO;

        public SelectionInfoType m_type;
        public int m_curHealth;
        public int m_PKDamage;
        public int m_maxHealth;
        public int m_curVigor;
        public int m_PKVigorloss;
        public int m_maxVigor;

        public SelectionInfo() {

        }

        public SelectionInfo(AC2Reader data) {
            m_type = (SelectionInfoType)data.ReadUInt32();
            if (m_type == SelectionInfoType.AGENT) {
                m_curHealth = data.ReadInt32();
                m_PKDamage = data.ReadInt32();
                m_maxHealth = data.ReadInt32();
                m_curVigor = data.ReadInt32();
                m_PKVigorloss = data.ReadInt32();
                m_maxVigor = data.ReadInt32();
            }
        }

        public void write(AC2Writer data, PackageRegistry registry) {
            data.Write((uint)m_type);
            if (m_type == SelectionInfoType.AGENT) {
                data.Write(m_curHealth);
                data.Write(m_PKDamage);
                data.Write(m_maxHealth);
                data.Write(m_curVigor);
                data.Write(m_PKVigorloss);
                data.Write(m_maxVigor);
            }
        }
    }
}
