using AC2E.Def;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AC2E.Protocol {

    public class LoginMinCharSetMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.OUT_OF_WORLD;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Login__MinCharSet_ID;

        // MinimalCharacterSet
        public uint numAllowedCharacters; // m_numAllowedCharacters
        public string accountName; // m_account
        public List<string> characterNames; // m_names
        public List<InstanceId> characterIds; // m_iids

        public LoginMinCharSetMsg() {

        }

        public LoginMinCharSetMsg(BinaryReader data) {
            numAllowedCharacters = data.ReadUInt32();
            accountName = data.ReadEncryptedString();
            characterNames = data.ReadList(() => data.ReadEncryptedString(Encoding.Unicode));
            characterIds = data.ReadList(data.ReadInstanceId);
        }

        public void write(BinaryWriter data) {
            data.Write(numAllowedCharacters);
            data.WriteEncryptedString(accountName);
            data.Write(characterNames, v => data.WriteEncryptedString(v, Encoding.Unicode));
            data.Write(characterIds, data.Write);
        }
    }
}
