using System.IO;

namespace AC2E.Def {

    public class FollowSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Player__Follow;

        // WM_Player::SendSEvt_Follow
        public InstanceId _target;

        public FollowSEvt(BinaryReader data) {
            _target = data.UnpackInstanceId();
        }
    }
}
