using System.IO;

namespace AC2E.Def {

    public class UpdateFactionStatusCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.PK__Faction_UpdateStatus;

        // WM_PK::PostCEvt_Faction_UpdateStatus
        public uint _newStatus;

        public UpdateFactionStatusCEvt() {

        }

        public UpdateFactionStatusCEvt(BinaryReader data) {
            _newStatus = data.UnpackUInt32();
        }

        public void write(BinaryWriter data) {
            data.Pack(_newStatus);
        }
    }
}
