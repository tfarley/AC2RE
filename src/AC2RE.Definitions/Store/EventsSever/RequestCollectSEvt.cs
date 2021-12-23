namespace AC2RE.Definitions;

public class RequestCollectSEvt : IServerEvent {

    public ServerEventFunctionId funcId => ServerEventFunctionId.Store__RequestCollect;

    // WM_Store::SendSEvt_Store_RequestCollect
    public bool profits; // _bProfits
    public uint target; // _target
    public InstanceId storekeeperId; // _iidStorekeeper

    public RequestCollectSEvt(AC2Reader data) {
        profits = data.UnpackBoolean();
        target = data.UnpackUInt32();
        storekeeperId = data.UnpackInstanceId();
    }
}
