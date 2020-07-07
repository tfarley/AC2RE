using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class UpdateFellowCellIdCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Fellowship__UpdateFellowCellID;

        // WM_Fellowship::PostCEvt_UpdateFellowCellID
        public CellId _value;
        public InstanceId _fid;

        public UpdateFellowCellIdCEvt() {

        }

        public UpdateFellowCellIdCEvt(BinaryReader data) {
            _value = new CellId(data.UnpackUInt32());
            _fid = data.UnpackInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_value.id);
            data.Pack(_fid);
        }
    }
}
