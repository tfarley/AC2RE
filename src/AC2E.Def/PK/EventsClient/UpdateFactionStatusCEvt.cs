namespace AC2E.Def {

    public class UpdateFactionStatusCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.PK__Faction_UpdateStatus;

        // WM_PK::PostCEvt_Faction_UpdateStatus
        public uint status; // _newStatus

        public UpdateFactionStatusCEvt() {

        }

        public UpdateFactionStatusCEvt(AC2Reader data) {
            status = data.UnpackUInt32();
        }

        public void write(AC2Writer data) {
            data.Pack(status);
        }
    }
}
