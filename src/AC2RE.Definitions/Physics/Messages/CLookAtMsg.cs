namespace AC2RE.Definitions;

public class CLookAtMsg : INetMessage {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
    public NetQueue queueId => NetQueue.WEENIE;
    public MessageOpcode opcode => MessageOpcode.Physics__CLookAt;
    public OrderingType orderingType => OrderingType.PRIVATE_ORDERED;

    // ECM_Physics::SendEvt_CLookAt
    public InstanceId targetId; // _target_id

    public CLookAtMsg(AC2Reader data) {
        targetId = data.ReadInstanceId();
    }
}
