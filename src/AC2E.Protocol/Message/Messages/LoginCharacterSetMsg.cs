using AC2E.Def.Extensions;
using AC2E.Def.Structs;
using AC2E.Protocol.NetBlob;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Protocol.Message.Messages {

    public class LoginCharacterSetMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.OUT_OF_WORLD;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Login__CharacterSet_ID;

        // CharacterSet
        public List<CharacterIdentity> characters; // set_
        public List<CharacterIdentity> deletedCharacters; // delSet_
        public uint status; // status_
        public uint numAllowedCharacters; // numAllowedCharacters_
        public string accountName; // account_
        public uint unk1;
        public bool hasLegions;
        public bool useTurbineChat; // m_fUseTurbineChat

        public LoginCharacterSetMsg() {

        }

        public LoginCharacterSetMsg(BinaryReader data) {
            characters = data.ReadList(() => new CharacterIdentity(data));
            deletedCharacters = data.ReadList(() => new CharacterIdentity(data));
            status = data.ReadUInt32();
            numAllowedCharacters = data.ReadUInt32();
            accountName = data.ReadEncryptedString();
            unk1 = data.ReadUInt32();
            hasLegions = data.ReadUInt32() != 0;
            useTurbineChat = data.ReadUInt32() != 0;
        }

        public void write(BinaryWriter data) {
            data.Write(characters, v => v.write(data));
            data.Write(deletedCharacters, v => v.write(data));
            data.Write(status);
            data.Write(numAllowedCharacters);
            data.WriteEncryptedString(accountName);
            data.Write(unk1);
            data.Write((uint)(hasLegions ? 1 : 0));
            data.Write((uint)(useTurbineChat ? 1 : 0)); // TODO: Double check these three flags - this might actually be the first "1"
        }
    }
}
