namespace AC2E.Def {

    public class UpdateFellowCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Fellowship__UpdateFellow;

        // WM_Fellowship::PostCEvt_UpdateFellow
        public Fellow _fellow;
        public InstanceId _fid;

        public UpdateFellowCEvt() {

        }

        public UpdateFellowCEvt(AC2Reader data) {
            _fellow = data.UnpackPackage<Fellow>();
            _fid = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(_fellow);
            data.Pack(_fid);
        }
    }
}
