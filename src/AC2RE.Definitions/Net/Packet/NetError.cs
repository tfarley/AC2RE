namespace AC2RE.Definitions;

public class NetError {

    // NetError
    public uint stringId; // m_stringID
    public EnumId tableId; // m_tableID

    public NetError(AC2Reader data) {
        stringId = data.ReadUInt32();
        tableId = data.ReadEnumId();
    }

    public void write(AC2Writer data) {
        data.Write(stringId);
        data.Write(tableId);
    }
}
