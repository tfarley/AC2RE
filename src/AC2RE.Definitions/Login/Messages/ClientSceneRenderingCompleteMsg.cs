namespace AC2RE.Definitions;

public class ClientSceneRenderingCompleteMsg : INetMessage {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
    public NetQueue queueId => NetQueue.WEENIE;
    public MessageOpcode opcode => MessageOpcode.Login__ClientSceneRenderingComplete;
    public OrderingType orderingType => OrderingType.UNORDERED;

    // ECM_Login::SendEvt_ClientSceneRenderingComplete

    public void write(AC2Writer data) {

    }
}
