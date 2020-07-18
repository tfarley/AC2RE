using AC2E.Def;
using System.IO;

namespace AC2E.Protocol {

    public class CharacterDeletionSMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.LOGON;
        public MessageOpcode opcode => MessageOpcode.Evt_Login__CharacterDeletion_ID;

        // ECM_Login::RecvEvt_CharacterDeletion
        public string accountName;
        public InstanceId characterId; // id

        public CharacterDeletionSMsg(BinaryReader data) {
            accountName = data.ReadEncryptedString();
            characterId = data.ReadInstanceId();
        }
    }
}
