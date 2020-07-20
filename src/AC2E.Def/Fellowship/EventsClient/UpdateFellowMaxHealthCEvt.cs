namespace AC2E.Def {

    public class UpdateFellowMaxHealthCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Fellowship__UpdateFellowMaxHealth;

        // WM_Fellowship::PostCEvt_UpdateFellowMaxHealth
        public uint _value;
        public InstanceId _fid;

        public UpdateFellowMaxHealthCEvt() {

        }

        public UpdateFellowMaxHealthCEvt(AC2Reader data) {
            _value = data.UnpackUInt32();
            _fid = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(_value);
            data.Pack(_fid);
        }
    }
}
