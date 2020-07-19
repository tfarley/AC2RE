using AC2E.Def;
using System.IO;

namespace AC2E.Def {

    public class CharacterDeletionCMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.OUT_OF_WORLD;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Login__CharacterDeletion_ID;

        // ECM_Login::RecvEvt_CharacterDeletion
        public InstanceId characterId; // id

        public CharacterDeletionCMsg(BinaryReader data) {
            characterId = data.ReadInstanceId();
        }
    }
}
