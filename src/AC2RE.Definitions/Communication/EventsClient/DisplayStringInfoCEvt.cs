namespace AC2RE.Definitions;

public class DisplayStringInfoCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Communication__DisplayStringInfo;

    // WM_Communication::PostCEvt_DisplayStringInfo
    public StringInfo text; // _msg
    public TextType type; // _type

    public DisplayStringInfoCEvt() {

    }

    public DisplayStringInfoCEvt(AC2Reader data) {
        text = data.UnpackHeapObject<StringInfo>();
        type = data.UnpackEnum<TextType>();
    }

    public void write(AC2Writer data) {
        data.Pack(text);
        data.PackEnum(type);
    }
}
