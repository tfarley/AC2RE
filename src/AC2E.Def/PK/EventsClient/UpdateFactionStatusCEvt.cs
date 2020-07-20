namespace AC2E.Def {

    public class UpdateFactionStatusCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.PK__Faction_UpdateStatus;

        // WM_PK::PostCEvt_Faction_UpdateStatus
        public uint _newStatus;

        public UpdateFactionStatusCEvt() {

        }

        public UpdateFactionStatusCEvt(AC2Reader data) {
            _newStatus = data.UnpackUInt32();
        }

        public void write(AC2Writer data) {
            data.Pack(_newStatus);
        }
    }
}
