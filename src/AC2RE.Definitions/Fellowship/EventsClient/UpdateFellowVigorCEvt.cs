namespace AC2RE.Definitions {

    public class UpdateFellowVigorCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Fellowship__UpdateFellowVigor;

        // WM_Fellowship::PostCEvt_UpdateFellowVigor
        public uint vigorPk; // _valuePK
        public uint vigor; // _value
        public InstanceId fellowId; // _fid

        public UpdateFellowVigorCEvt() {

        }

        public UpdateFellowVigorCEvt(AC2Reader data) {
            vigorPk = data.UnpackUInt32();
            vigor = data.UnpackUInt32();
            fellowId = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(vigorPk);
            data.Pack(vigor);
            data.Pack(fellowId);
        }
    }
}
