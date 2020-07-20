namespace AC2E.Def {

    public class UpdateFellowCellIdCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Fellowship__UpdateFellowCellID;

        // WM_Fellowship::PostCEvt_UpdateFellowCellID
        public CellId _value;
        public InstanceId _fid;

        public UpdateFellowCellIdCEvt() {

        }

        public UpdateFellowCellIdCEvt(AC2Reader data) {
            _value = new CellId(data.UnpackUInt32());
            _fid = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(_value.id);
            data.Pack(_fid);
        }
    }
}
