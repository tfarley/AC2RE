namespace AC2E.Def {

    public class InscribeItemSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Player__InscribeItem;

        // WM_Player::SendSEvt_InscribeItem
        public StringInfo _siInscription;
        public InstanceId _iidTarget;

        public InscribeItemSEvt(AC2Reader data) {
            _siInscription = data.UnpackPackage<StringInfo>();
            _iidTarget = data.UnpackInstanceId();
        }
    }
}
