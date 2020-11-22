namespace AC2RE.Definitions {

    public class InscribeItemSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Player__InscribeItem;

        // WM_Player::SendSEvt_InscribeItem
        public StringInfo inscriptionText; // _siInscription
        public InstanceId targetId; // _iidTarget

        public InscribeItemSEvt(AC2Reader data) {
            inscriptionText = data.UnpackPackage<StringInfo>();
            targetId = data.UnpackInstanceId();
        }
    }
}
