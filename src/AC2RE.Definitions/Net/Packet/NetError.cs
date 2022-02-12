namespace AC2RE.Definitions;

public class NetError {

    // NetError
    public StringId stringId; // m_stringID
    public EnumId tableEnumId; // m_tableID

    public NetError(AC2Reader data) {
        stringId = data.ReadStringId();
        tableEnumId = data.ReadEnumId();
    }

    public void write(AC2Writer data) {
        data.Write(stringId);
        data.Write(tableEnumId);
    }
}
