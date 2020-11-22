namespace AC2RE.Definitions {

    public class SetSelectionSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Player__SetSelection;

        // WM_Player::SendSEvt_SetSelection
        public InstanceId selectionId; // _selectionID

        public SetSelectionSEvt(AC2Reader data) {
            selectionId = data.UnpackInstanceId();
        }
    }
}
