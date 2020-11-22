namespace AC2RE.Definitions {

    public class PlayerDescMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Login__PlayerDesc_ID;

        // ECM_Login::RecvEvt_PlayerDesc
        public CBaseQualities qualities; // _q

        public PlayerDescMsg() {

        }

        public PlayerDescMsg(AC2Reader data) {
            qualities = new(data);
        }

        public void write(AC2Writer data) {
            qualities.write(data);
        }
    }
}
