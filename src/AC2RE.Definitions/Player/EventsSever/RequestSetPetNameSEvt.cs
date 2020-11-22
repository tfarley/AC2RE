namespace AC2RE.Definitions {

    public class RequestSetPetNameSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Player__RequestSetPetName;

        // WM_Player::SendSEvt_RequestSetPetName
        public WPString petName; // _sPetName
        public InstanceId petId; // _iidPet

        public RequestSetPetNameSEvt(AC2Reader data) {
            petName = data.UnpackPackage<WPString>();
            petId = data.UnpackInstanceId();
        }
    }
}
