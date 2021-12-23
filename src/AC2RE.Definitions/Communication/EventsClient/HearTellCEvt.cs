namespace AC2RE.Definitions;

public class HearTellCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Communication__CHearTell;

    // WM_Communication::PostCEvt_CHearTell
    public uint weenieChatFlags; // _weenieChatFlags
    public StringInfo text; // _msg
    public StringInfo tellerName; // _teller
    public InstanceId tellerId; // _tellerID

    public HearTellCEvt() {

    }

    public HearTellCEvt(AC2Reader data) {
        weenieChatFlags = data.UnpackUInt32();
        text = data.UnpackPackage<StringInfo>();
        tellerName = data.UnpackPackage<StringInfo>();
        tellerId = data.UnpackInstanceId();
    }

    public void write(AC2Writer data) {
        data.Pack(weenieChatFlags);
        data.Pack(text);
        data.Pack(tellerName);
        data.Pack(tellerId);
    }
}
