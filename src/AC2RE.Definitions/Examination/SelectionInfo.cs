namespace AC2RE.Definitions;

public class SelectionInfo : IHeapObject {

    public NativeType nativeType => NativeType.SelectionInfo;

    // Const *_SelectionInfoType
    public enum InfoType : uint {
        Undef = 0, // Undef_SelectionInfoType

        Agent = 0x40000001, // Agent_SelectionInfoType
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
        type = data.ReadEnum<InfoType>();
        if (type == InfoType.Agent) {
            curHealth = data.ReadInt32();
            pkDamage = data.ReadInt32();
            maxHealth = data.ReadInt32();
            curVigor = data.ReadInt32();
            pkVigorloss = data.ReadInt32();
            maxVigor = data.ReadInt32();
        }
    }

    public void write(AC2Writer data) {
        data.WriteEnum(type);
        if (type == InfoType.Agent) {
            data.Write(curHealth);
            data.Write(pkDamage);
            data.Write(maxHealth);
            data.Write(curVigor);
            data.Write(pkVigorloss);
            data.Write(maxVigor);
        }
    }
}
