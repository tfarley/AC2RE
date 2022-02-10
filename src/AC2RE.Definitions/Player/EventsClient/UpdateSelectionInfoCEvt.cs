namespace AC2RE.Definitions;

public class UpdateSelectionInfoCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Player__UpdateSelectionInfo;

    // WM_Player::PostCEvt_UpdateSelectionInfo
    public SelectionInfo info; // _info
    public InstanceId selectionId; // _selectionID

    public UpdateSelectionInfoCEvt() {

    }

    public UpdateSelectionInfoCEvt(AC2Reader data) {
        info = data.UnpackHeapObject<SelectionInfo>();
        selectionId = data.UnpackInstanceId();
    }

    public void write(AC2Writer data) {
        data.Pack(info);
        data.Pack(selectionId);
    }
}
