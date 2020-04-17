using AC2E.Def.Structs;
using AC2E.Protocol.NetBlob;
using AC2E.Utils.Extensions;
using System.IO;
using System.Text;

namespace AC2E.Protocol.Messages {

    public class LoginMinCharSetMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.OUT_OF_WORLD;

        public NetQueue queueId => NetQueue.NET_QUEUE_EVENT;

        public MessageOpcode opcode => MessageOpcode.Evt_Login__MinCharSet_ID;

        public string accountName;
        public CharacterIdentity[] characters;

        public void write(BinaryWriter data) {
            data.Write((uint)0); // TODO: Unknown
            data.WriteEncryptedString(accountName);
            data.Write((uint)characters.Length);
            foreach (CharacterIdentity character in characters) {
                data.WriteEncryptedString(character.name, Encoding.Unicode);
            }
            data.Write((uint)characters.Length);
            foreach (CharacterIdentity character in characters) {
                data.Write(character.id);
            }
        }
    }
}
