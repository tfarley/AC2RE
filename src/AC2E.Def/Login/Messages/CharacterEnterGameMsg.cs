using AC2E.Def;
using System.IO;

namespace AC2E.Def {

    public class CharacterEnterGameMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.LOGON;
        public MessageOpcode opcode => MessageOpcode.CHARACTER_ENTER_GAME_EVENT;

        public InstanceId characterId;
        public string accountName;

        public CharacterEnterGameMsg(BinaryReader data) {
            characterId = data.ReadInstanceId();
            accountName = data.ReadEncryptedString();
        }
    }
}
