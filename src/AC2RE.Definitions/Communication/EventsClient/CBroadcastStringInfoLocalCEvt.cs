namespace AC2RE.Definitions;

public class CBroadcastStringInfoLocalCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Communication__CBroadcastStringInfoLocal;

    // WM_Communication::PostCEvt_CBroadcastStringInfoLocal
    public StringInfo text; // _msg
    public TextType type; // _type

    public CBroadcastStringInfoLocalCEvt() {

    }

    public CBroadcastStringInfoLocalCEvt(AC2Reader data) {
        text = data.UnpackHeapObject<StringInfo>();
        type = data.UnpackEnum<TextType>();
    }

    public void write(AC2Writer data) {
        data.Pack(text);
        data.PackEnum(type);
    }
}
