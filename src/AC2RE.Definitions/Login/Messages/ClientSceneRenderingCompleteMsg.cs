namespace AC2RE.Definitions {

    public class ClientSceneRenderingCompleteMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.WEENIE;
        public MessageOpcode opcode => MessageOpcode.Login__ClientSceneRenderingComplete;

        // ECM_Login::SendEvt_ClientSceneRenderingComplete

        public void write(AC2Writer data) {

        }
    }
}
