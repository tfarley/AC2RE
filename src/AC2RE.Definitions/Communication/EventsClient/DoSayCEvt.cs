namespace AC2RE.Definitions;

public class DoSayCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Communication__CDoSay;

    // WM_Communication::PostCEvt_CDoSay
    public uint weenieChatFlags; // _weenieChatFlags
    public StringInfo text; // _msg

    public DoSayCEvt() {

    }

    public DoSayCEvt(AC2Reader data) {
        weenieChatFlags = data.UnpackUInt32();
        text = data.UnpackHeapObject<StringInfo>();
    }

    public void write(AC2Writer data) {
        data.Pack(weenieChatFlags);
        data.Pack(text);
    }
}
