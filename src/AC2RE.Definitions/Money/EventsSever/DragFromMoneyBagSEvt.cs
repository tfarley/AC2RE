namespace AC2RE.Definitions;

public class DragFromMoneyBagSEvt : IServerEvent {

    public ServerEventFunctionId funcId => ServerEventFunctionId.Money__DragFromMoneyBag;

    // WM_Money::SendSEvt_DragFromMoneyBag
    public InstanceId containerId; // _iidToContainer
    public uint toSlot; // _toSlot
    public uint quantity; // _quantity

    public DragFromMoneyBagSEvt(AC2Reader data) {
        containerId = data.UnpackInstanceId();
        toSlot = data.UnpackUInt32();
        quantity = data.UnpackUInt32();
    }
}
