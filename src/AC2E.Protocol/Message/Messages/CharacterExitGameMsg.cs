using AC2E.Def;
using System.IO;

namespace AC2E.Protocol {

    public class CharacterExitGameMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.LOGON;
        public MessageOpcode opcode => MessageOpcode.Evt_Login__CharExitGame_ID;

        public InstanceId characterId;

        public CharacterExitGameMsg(BinaryReader data) {
            characterId = data.ReadInstanceId();
        }
    }
}
