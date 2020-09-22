namespace AC2E.Def {

    public class CLookAtDirMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.WEENIE;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__CLookAtDir_ID;

        // ECM_Physics::SendEvt_CLookAtDir
        public float x; // _x
        public float z; // _y

        public CLookAtDirMsg(AC2Reader data) {
            x = data.ReadSingle();
            z = data.ReadSingle();
        }
    }
}
