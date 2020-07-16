using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class UpdateFellowMaxVigorCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Fellowship__UpdateFellowMaxVigor;

        // WM_Fellowship::PostCEvt_UpdateFellowMaxVigor
        public uint _value;
        public InstanceId _fid;

        public UpdateFellowMaxVigorCEvt() {

        }

        public UpdateFellowMaxVigorCEvt(BinaryReader data) {
            _value = data.UnpackUInt32();
            _fid = data.UnpackInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_value);
            data.Pack(_fid);
        }
    }
}
