namespace AC2RE.Definitions;

public class DragToMoneyBagDoneCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Money__DragToMoneyBag_Done;

    // WM_Money::PostCEvt_DragToMoneyBag_Done
    public InstanceId fromContainerId; // _iidFromContainer
    public uint fromSlot; // _fromSlot
    public InstanceId itemId; // _iidItem

    public DragToMoneyBagDoneCEvt() {

    }

    public DragToMoneyBagDoneCEvt(AC2Reader data) {
        fromContainerId = data.UnpackInstanceId();
        fromSlot = data.UnpackUInt32();
        itemId = data.UnpackInstanceId();
    }

    public void write(AC2Writer data) {
        data.Pack(fromContainerId);
        data.Pack(fromSlot);
        data.Pack(itemId);
    }
}
