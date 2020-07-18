using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class ForceExamineItemCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Player__ForceExamineItem;

        // WM_Player::PostCEvt_ForceExamineItem
        public InstanceId _targetID;

        public ForceExamineItemCEvt() {

        }

        public ForceExamineItemCEvt(BinaryReader data) {
            _targetID = data.UnpackInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_targetID);
        }
    }
}
