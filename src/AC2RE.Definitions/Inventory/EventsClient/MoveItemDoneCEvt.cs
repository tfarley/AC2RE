namespace AC2RE.Definitions;

public class MoveItemDoneCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Inventory__MoveItem_Done;

    // WM_Inventory::PostCEvt_MoveItem_Done
    public InvMoveDesc moveDesc; // _iDesc

    public MoveItemDoneCEvt() {

    }

    public MoveItemDoneCEvt(AC2Reader data) {
        moveDesc = data.UnpackPackage<InvMoveDesc>();
    }

    public void write(AC2Writer data) {
        data.Pack(moveDesc);
    }
}
