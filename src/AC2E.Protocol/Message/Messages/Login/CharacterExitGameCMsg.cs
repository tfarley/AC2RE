using System.IO;

namespace AC2E.Protocol {

    public class CharacterExitGameCMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.LOGON;
        public MessageOpcode opcode => MessageOpcode.Evt_Login__CharExitGame_ID;

        public CharacterExitGameCMsg(BinaryReader data) {

        }
    }
}
