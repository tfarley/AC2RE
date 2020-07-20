namespace AC2E.Def {

    public class UpdateSelectionInfoCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Player__UpdateSelectionInfo;

        // WM_Player::PostCEvt_UpdateSelectionInfo
        public SelectionInfo _info;
        public InstanceId _selectionID;

        public UpdateSelectionInfoCEvt() {

        }

        public UpdateSelectionInfoCEvt(AC2Reader data) {
            _info = data.UnpackPackage<SelectionInfo>();
            _selectionID = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(_info);
            data.Pack(_selectionID);
        }
    }
}
