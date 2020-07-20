namespace AC2E.Def {

    public class SetSelectionSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Player__SetSelection;

        // WM_Player::SendSEvt_SetSelection
        public InstanceId _selectionID;

        public SetSelectionSEvt(AC2Reader data) {
            _selectionID = data.UnpackInstanceId();
        }
    }
}
