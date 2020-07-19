using System.IO;

namespace AC2E.Def {

    public class RequestPetAttackSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Player__RequestPetAttack;

        // WM_Player::SendSEvt_RequestPetAttack
        public InstanceId _targetID;

        public RequestPetAttackSEvt(BinaryReader data) {
            _targetID = data.UnpackInstanceId();
        }
    }
}
