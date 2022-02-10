namespace AC2RE.Definitions;

public class ReorganizeContentsDoneCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Inventory__ReorganizeContents_Done;

    // WM_Inventory::PostCEvt_ReorganizeContents_Done
    public InvMoveDesc moveDesc; // _iDesc

    public ReorganizeContentsDoneCEvt() {

    }

    public ReorganizeContentsDoneCEvt(AC2Reader data) {
        moveDesc = data.UnpackHeapObject<InvMoveDesc>();
    }

    public void write(AC2Writer data) {
        data.Pack(moveDesc);
    }
}
