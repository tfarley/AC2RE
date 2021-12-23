namespace AC2RE.Definitions;

public class TellSEvt : IServerEvent {

    public ServerEventFunctionId funcId => ServerEventFunctionId.Communication__Tell;

    // WM_Communication::SendSEvt_Tell
    public uint weenieChatFlags; // _weenieChatFlags
    public StringInfo text; // _msg
    public StringInfo telleeName; // _tellee

    public TellSEvt(AC2Reader data) {
        weenieChatFlags = data.UnpackUInt32();
        text = data.UnpackPackage<StringInfo>();
        telleeName = data.UnpackPackage<StringInfo>();
    }
}
