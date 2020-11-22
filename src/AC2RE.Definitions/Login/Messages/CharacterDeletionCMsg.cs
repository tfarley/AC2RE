namespace AC2RE.Definitions {

    public class CharacterDeletionCMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.OUT_OF_WORLD;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Login__CharacterDeletion_ID;

        // ECM_Login::RecvEvt_CharacterDeletion
        public InstanceId characterId; // id

        public CharacterDeletionCMsg() {

        }

        public CharacterDeletionCMsg(AC2Reader data) {
            characterId = data.ReadInstanceId();
        }

        public void write(AC2Writer data) {
            data.Write(characterId);
        }
    }
}
