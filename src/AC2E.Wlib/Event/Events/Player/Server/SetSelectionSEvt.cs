using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class SetSelectionSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Player__SetSelection;

        // WM_Player::SendSEvt_SetSelection
        public InstanceId _selectionID;

        public SetSelectionSEvt(BinaryReader data) {
            _selectionID = data.UnpackInstanceId();
        }
    }
}
