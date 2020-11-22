namespace AC2RE.Definitions {

    public class UpdateFellowCellIdCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Fellowship__UpdateFellowCellID;

        // WM_Fellowship::PostCEvt_UpdateFellowCellID
        public CellId cell; // _value
        public InstanceId fellowId; // _fid

        public UpdateFellowCellIdCEvt() {

        }

        public UpdateFellowCellIdCEvt(AC2Reader data) {
            cell = new(data.UnpackUInt32());
            fellowId = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(cell.id);
            data.Pack(fellowId);
        }
    }
}
