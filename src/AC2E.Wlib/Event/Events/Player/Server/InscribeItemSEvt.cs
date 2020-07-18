using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class InscribeItemSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Player__InscribeItem;

        // WM_Player::SendSEvt_InscribeItem
        public StringInfo _siInscription;
        public InstanceId _iidTarget;

        public InscribeItemSEvt(BinaryReader data) {
            _siInscription = data.UnpackPackage<StringInfo>();
            _iidTarget = data.UnpackInstanceId();
        }
    }
}
