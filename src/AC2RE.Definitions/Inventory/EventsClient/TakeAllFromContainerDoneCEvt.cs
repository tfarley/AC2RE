namespace AC2RE.Definitions;

public class TakeAllFromContainerDoneCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Inventory__TakeAllFromContainer_Done;

    // WM_Inventory::PostCEvt_TakeAllFromContainer_Done
    public InvTakeAllDesc takeAllDesc; // _iDesc

    public TakeAllFromContainerDoneCEvt() {

    }

    public TakeAllFromContainerDoneCEvt(AC2Reader data) {
        takeAllDesc = data.UnpackPackage<InvTakeAllDesc>();
    }

    public void write(AC2Writer data) {
        data.Pack(takeAllDesc);
    }
}
