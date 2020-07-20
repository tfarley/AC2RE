namespace AC2E.Def {

    public class CharacterDeletionSMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.LOGON;
        public MessageOpcode opcode => MessageOpcode.Evt_Login__CharacterDeletion_ID;

        // ECM_Login::RecvEvt_CharacterDeletion
        public string accountName;
        public InstanceId characterId; // id

        public CharacterDeletionSMsg(AC2Reader data) {
            accountName = data.ReadString();
            characterId = data.ReadInstanceId();
        }
    }
}
