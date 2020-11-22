using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class CharacterSetMsg : INetMessage {

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

        public CharacterSetMsg() {

        }

        public CharacterSetMsg(AC2Reader data) {
            characters = data.ReadList(() => new CharacterIdentity(data));
            deletedCharacters = data.ReadList(() => new CharacterIdentity(data));
            status = data.ReadUInt32();
            numAllowedCharacters = data.ReadUInt32();
            accountName = data.ReadString();
            unk1 = data.ReadUInt32();
            hasLegions = data.ReadBoolean();
            useTurbineChat = data.ReadBoolean();
        }

        public void write(AC2Writer data) {
            data.Write(characters, v => v.write(data));
            data.Write(deletedCharacters, v => v.write(data));
            data.Write(status);
            data.Write(numAllowedCharacters);
            data.Write(accountName);
            data.Write(unk1);
            data.Write(hasLegions);
            data.Write(useTurbineChat); // TODO: Double check these three flags - this might actually be the first "1"
        }
    }
}
