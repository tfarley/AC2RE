using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class UpdateFellowMaxHealthCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Fellowship__UpdateFellowMaxHealth;

        // WM_Fellowship::PostCEvt_UpdateFellowMaxHealth
        public uint _value;
        public InstanceId _fid;

        public UpdateFellowMaxHealthCEvt() {

        }

        public UpdateFellowMaxHealthCEvt(BinaryReader data) {
            _value = data.UnpackUInt32();
            _fid = data.UnpackInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_value);
            data.Pack(_fid);
        }
    }
}
