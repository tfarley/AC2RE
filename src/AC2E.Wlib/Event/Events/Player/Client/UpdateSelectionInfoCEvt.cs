using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class UpdateSelectionInfoCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Player__UpdateSelectionInfo;

        // WM_Player::PostCEvt_UpdateSelectionInfo
        public SelectionInfo _info;
        public InstanceId _selectionID;

        public UpdateSelectionInfoCEvt() {

        }

        public UpdateSelectionInfoCEvt(BinaryReader data) {
            _info = data.UnpackPackage<SelectionInfo>();
            _selectionID = data.UnpackInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_info);
            data.Pack(_selectionID);
        }
    }
}
