namespace AC2E.Def {

    public class ForceExamineItemCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Player__ForceExamineItem;

        // WM_Player::PostCEvt_ForceExamineItem
        public InstanceId _targetID;

        public ForceExamineItemCEvt() {

        }

        public ForceExamineItemCEvt(AC2Reader data) {
            _targetID = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(_targetID);
        }
    }
}
