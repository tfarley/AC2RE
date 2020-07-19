using System.IO;

namespace AC2E.Def {

    public class UpdateFellowVigorCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Fellowship__UpdateFellowVigor;

        // WM_Fellowship::PostCEvt_UpdateFellowVigor
        public uint _valuePK;
        public uint _value;
        public InstanceId _fid;

        public UpdateFellowVigorCEvt() {

        }

        public UpdateFellowVigorCEvt(BinaryReader data) {
            _valuePK = data.UnpackUInt32();
            _value = data.UnpackUInt32();
            _fid = data.UnpackInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_valuePK);
            data.Pack(_value);
            data.Pack(_fid);
        }
    }
}
