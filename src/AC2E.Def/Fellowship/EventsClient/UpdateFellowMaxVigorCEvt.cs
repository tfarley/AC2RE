namespace AC2E.Def {

    public class UpdateFellowMaxVigorCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Fellowship__UpdateFellowMaxVigor;

        // WM_Fellowship::PostCEvt_UpdateFellowMaxVigor
        public uint _value;
        public InstanceId _fid;

        public UpdateFellowMaxVigorCEvt() {

        }

        public UpdateFellowMaxVigorCEvt(AC2Reader data) {
            _value = data.UnpackUInt32();
            _fid = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(_value);
            data.Pack(_fid);
        }
    }
}
