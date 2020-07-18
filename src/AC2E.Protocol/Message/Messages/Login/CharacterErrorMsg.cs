using AC2E.Def;
using System.IO;

namespace AC2E.Protocol {

    public class CharacterErrorMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.OUT_OF_WORLD;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Login__CharacterError_ID;

        // ECM_Login::RecvEvt_CharacterError
        public CharError error; // error

        public CharacterErrorMsg(BinaryReader data) {
            error = (CharError)data.ReadUInt32();
        }
    }
}
