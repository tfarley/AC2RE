namespace AC2RE.Definitions;

public class TransmuteAllFromContainerDoneCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Inventory__TransmuteAllFromContainer_Done;

    // WM_Inventory::PostCEvt_TransmuteAllFromContainer_Done
    public InvTransmuteAllDesc transmuteDesc; // _iDesc

    public TransmuteAllFromContainerDoneCEvt() {

    }

    public TransmuteAllFromContainerDoneCEvt(AC2Reader data) {
        transmuteDesc = data.UnpackHeapObject<InvTransmuteAllDesc>();
    }

    public void write(AC2Writer data) {
        data.Pack(transmuteDesc);
    }
}
