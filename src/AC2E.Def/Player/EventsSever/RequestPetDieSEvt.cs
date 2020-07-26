namespace AC2E.Def {

    public class RequestPetDieSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Player__RequestPetDie;

        // WM_Player::SendSEvt_RequestPetDie
        public InstanceId petId; // _iidPet

        public RequestPetDieSEvt(AC2Reader data) {
            petId = data.UnpackInstanceId();
        }
    }
}
