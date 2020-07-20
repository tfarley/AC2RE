namespace AC2E.Def {

    public class RequestSetPetNameSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Player__RequestSetPetName;

        // WM_Player::SendSEvt_RequestSetPetName
        public WPString _sPetName;
        public InstanceId _iidPet;

        public RequestSetPetNameSEvt(AC2Reader data) {
            _sPetName = data.UnpackPackage<WPString>();
            _iidPet = data.UnpackInstanceId();
        }
    }
}
