namespace AC2RE.Definitions;

public class RequestSellSEvt : IServerEvent {

    public ServerEventFunctionId funcId => ServerEventFunctionId.Store__RequestSell;

    // WM_Store::SendSEvt_Store_RequestSell
    public DataId storeDid; // _targetStore
    public uint price; // _price
    public uint quantity; // _quantity
    public InstanceId itemId; // _iidItem
    public InstanceId storekeeperId; // _iidStorekeeper

    public RequestSellSEvt(AC2Reader data) {
        storeDid = data.UnpackDataId();
        price = data.UnpackUInt32();
        quantity = data.UnpackUInt32();
        itemId = data.UnpackInstanceId();
        storekeeperId = data.UnpackInstanceId();
    }
}
