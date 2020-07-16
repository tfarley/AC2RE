using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class RequestPetDieSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Player__RequestPetDie;

        // WM_Player::SendSEvt_RequestPetDie
        public InstanceId _iidPet;

        public RequestPetDieSEvt(BinaryReader data) {
            _iidPet = data.UnpackInstanceId();
        }
    }
}
