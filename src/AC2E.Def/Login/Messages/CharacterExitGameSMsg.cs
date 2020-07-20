namespace AC2E.Def {

    public class CharacterExitGameSMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Login__CharExitGame_ID;

        // ECM_Login::RecvEvt_CharExitGame
        public InstanceId characterId;

        public CharacterExitGameSMsg(AC2Reader data) {
            characterId = data.ReadInstanceId();
        }
    }
}
