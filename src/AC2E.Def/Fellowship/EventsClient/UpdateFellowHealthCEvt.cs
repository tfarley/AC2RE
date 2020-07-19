using System.IO;

namespace AC2E.Def {

    public class UpdateFellowHealthCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Fellowship__UpdateFellowHealth;

        // WM_Fellowship::PostCEvt_UpdateFellowHealth
        public uint _valuePK;
        public uint _value;
        public InstanceId _fid;

        public UpdateFellowHealthCEvt() {

        }

        public UpdateFellowHealthCEvt(BinaryReader data) {
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
