using System.IO;

namespace AC2E.Def {

    public class UpdateAllegianceChatDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Allegiance__UpdateAllegianceChatDone;

        // WM_Allegiance::PostCEvt_UpdateAllegianceChatDone
        public uint _roomID;

        public UpdateAllegianceChatDoneCEvt() {

        }

        public UpdateAllegianceChatDoneCEvt(BinaryReader data) {
            _roomID = data.UnpackUInt32();
        }

        public void write(BinaryWriter data) {
            data.Pack(_roomID);
        }
    }
}
