using AC2E.Def;
using System.IO;

namespace AC2E.Protocol {

    public class CharacterExitGameCMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Login__CharExitGame_ID;

        public InstanceId characterId;

        public CharacterExitGameCMsg(BinaryReader data) {
            characterId = data.ReadInstanceId();
        }
    }
}
