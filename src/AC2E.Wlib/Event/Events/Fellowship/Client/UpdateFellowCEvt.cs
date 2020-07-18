using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class UpdateFellowCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Fellowship__UpdateFellow;

        // WM_Fellowship::PostCEvt_UpdateFellow
        public FellowPkg _fellow;
        public InstanceId _fid;

        public UpdateFellowCEvt() {

        }

        public UpdateFellowCEvt(BinaryReader data) {
            _fellow = data.UnpackPackage<FellowPkg>();
            _fid = data.UnpackInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_fellow);
            data.Pack(_fid);
        }
    }
}
