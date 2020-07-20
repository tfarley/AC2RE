namespace AC2E.Def {

    public class UpdateAllegianceChatDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Allegiance__UpdateAllegianceChatDone;

        // WM_Allegiance::PostCEvt_UpdateAllegianceChatDone
        public uint _roomID;

        public UpdateAllegianceChatDoneCEvt() {

        }

        public UpdateAllegianceChatDoneCEvt(AC2Reader data) {
            _roomID = data.UnpackUInt32();
        }

        public void write(AC2Writer data) {
            data.Pack(_roomID);
        }
    }
}
