using AC2E.Def.Extensions;
using AC2E.Def.Structs;
using AC2E.Protocol.NetBlob;
using System.IO;

namespace AC2E.Protocol.Message.Messages {

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
