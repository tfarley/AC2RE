namespace AC2E.Def {

    public class UpdateFellowLevelCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Fellowship__UpdateFellowLevel;

        // WM_Fellowship::PostCEvt_UpdateFellowLevel
        public uint _new_level;
        public InstanceId _fid;

        public UpdateFellowLevelCEvt() {

        }

        public UpdateFellowLevelCEvt(AC2Reader data) {
            _new_level = data.UnpackUInt32();
            _fid = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(_new_level);
            data.Pack(_fid);
        }
    }
}
