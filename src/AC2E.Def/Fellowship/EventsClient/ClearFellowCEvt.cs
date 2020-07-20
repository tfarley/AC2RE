namespace AC2E.Def {

    public class ClearFellowCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Fellowship__ClearFellow;

        // WM_Fellowship::PostCEvt_ClearFellow
        public InstanceId _fid;

        public ClearFellowCEvt() {

        }

        public ClearFellowCEvt(AC2Reader data) {
            _fid = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(_fid);
        }
    }
}
