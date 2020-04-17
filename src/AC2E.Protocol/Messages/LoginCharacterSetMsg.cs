using AC2E.Def.Structs;
using AC2E.Protocol.NetBlob;
using AC2E.Utils.Extensions;
using System.IO;

namespace AC2E.Protocol.Messages {

    public class LoginCharacterSetMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.OUT_OF_WORLD;

        public NetQueue queueId => NetQueue.NET_QUEUE_EVENT;

        public MessageOpcode opcode => MessageOpcode.Evt_Login__CharacterSet_ID;

        public CharacterIdentity[] characters;
        public uint numAllowedCharacters;
        public string accountName;
        public bool hasLegions;
        public bool useTurbineChat;

        public void write(BinaryWriter data) {
            data.Write((uint)characters.Length);
            foreach (CharacterIdentity character in characters) {
                character.write(data);
            }
            data.Write((uint)0); // TODO: Unknown
            data.Write((uint)0); // TODO: Unknown
            data.Write(numAllowedCharacters);
            data.WriteEncryptedString(accountName);
            data.Write((uint)1); // TODO: Unknown
            data.Write((uint)(hasLegions ? 1 : 0));
            data.Write((uint)(useTurbineChat ? 1 : 0)); // TODO: Double check these three flags - this might actually be the first "1"
        }
    }
}
