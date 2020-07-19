using System.IO;

namespace AC2E.Def {

    public class ClientSceneRenderingCompleteMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.WEENIE;
        public MessageOpcode opcode => MessageOpcode.Evt_Login__ClientSceneRenderingComplete_ID;

        // ECM_Login::SendEvt_ClientSceneRenderingComplete

        public void write(BinaryWriter data) {

        }
    }
}
