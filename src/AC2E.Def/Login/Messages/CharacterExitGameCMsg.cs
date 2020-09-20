namespace AC2E.Def {

    public class CharacterExitGameCMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.LOGON;
        public MessageOpcode opcode => MessageOpcode.Evt_Login__CharExitGame_ID;

        public CharacterExitGameCMsg() {

        }

        public CharacterExitGameCMsg(AC2Reader data) {

        }

        public void write(AC2Writer data) {

        }
    }
}
