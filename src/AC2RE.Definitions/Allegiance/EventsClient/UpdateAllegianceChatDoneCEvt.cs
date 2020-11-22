namespace AC2RE.Definitions {

    public class UpdateAllegianceChatDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Allegiance__UpdateAllegianceChatDone;

        // WM_Allegiance::PostCEvt_UpdateAllegianceChatDone
        public uint roomId; // _roomID

        public UpdateAllegianceChatDoneCEvt() {

        }

        public UpdateAllegianceChatDoneCEvt(AC2Reader data) {
            roomId = data.UnpackUInt32();
        }

        public void write(AC2Writer data) {
            data.Pack(roomId);
        }
    }
}
