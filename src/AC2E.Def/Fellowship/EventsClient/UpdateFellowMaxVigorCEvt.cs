namespace AC2E.Def {

    public class UpdateFellowMaxVigorCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Fellowship__UpdateFellowMaxVigor;

        // WM_Fellowship::PostCEvt_UpdateFellowMaxVigor
        public uint maxVigor; // _value
        public InstanceId fellowId; // _fid

        public UpdateFellowMaxVigorCEvt() {

        }

        public UpdateFellowMaxVigorCEvt(AC2Reader data) {
            maxVigor = data.UnpackUInt32();
            fellowId = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(maxVigor);
            data.Pack(fellowId);
        }
    }
}
