namespace AC2E.Def {

    public class SelectionInfo : IPackage {

        public NativeType nativeType => NativeType.SELECTIONINFO;

        // Const *_SelectionInfoType
        public enum InfoType : uint {
            UNDEF = 0,
            AGENT = 0x40000001,
        }

        public InfoType type; // m_type
        public int curHealth; // m_curHealth
        public int pkDamage; // m_PKDamage
        public int maxHealth; // m_maxHealth
        public int curVigor; // m_curVigor
        public int pkVigorloss; // m_PKVigorloss
        public int maxVigor; // m_maxVigor

        public SelectionInfo() {

        }

        public SelectionInfo(AC2Reader data) {
            type = (InfoType)data.ReadUInt32();
            if (type == InfoType.AGENT) {
                curHealth = data.ReadInt32();
                pkDamage = data.ReadInt32();
                maxHealth = data.ReadInt32();
                curVigor = data.ReadInt32();
                pkVigorloss = data.ReadInt32();
                maxVigor = data.ReadInt32();
            }
        }

        public void write(AC2Writer data) {
            data.Write((uint)type);
            if (type == InfoType.AGENT) {
                data.Write(curHealth);
                data.Write(pkDamage);
                data.Write(maxHealth);
                data.Write(curVigor);
                data.Write(pkVigorloss);
                data.Write(maxVigor);
            }
        }
    }
}
