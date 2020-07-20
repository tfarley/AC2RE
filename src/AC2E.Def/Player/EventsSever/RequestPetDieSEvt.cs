namespace AC2E.Def {

    public class RequestPetDieSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Player__RequestPetDie;

        // WM_Player::SendSEvt_RequestPetDie
        public InstanceId _iidPet;

        public RequestPetDieSEvt(AC2Reader data) {
            _iidPet = data.UnpackInstanceId();
        }
    }
}
