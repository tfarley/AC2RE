using AC2E.Def.Extensions;
using AC2E.Protocol.NetBlob;
using System.IO;

namespace AC2E.Protocol.Messages {

    public class CharacterEnterGameMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;

        public NetQueue queueId => NetQueue.NET_QUEUE_LOGON;

        public MessageOpcode opcode => MessageOpcode.CHARACTER_ENTER_GAME_EVENT;

        public ulong characterId;
        public string accountName;

        public CharacterEnterGameMsg(BinaryReader data) {
            characterId = data.ReadUInt64();
            accountName = data.ReadEncryptedString();
        }
    }
}
