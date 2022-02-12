namespace AC2RE.Definitions;

public class ReleaseBehaviorMsg : INetMessage {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
    public NetQueue queueId => NetQueue.EVENT;
    public MessageOpcode opcode => MessageOpcode.Physics__ReleaseBehavior;
    public OrderingType orderingType => OrderingType.VISUAL_ORDERED;

    // ECM_Physics::RecvEvt_ReleaseBehavior
    public InstanceIdWithStamp senderIdWithStamp; // sender
    public BehaviorId behaviorId; // _behavior_id
    public BehaviorParams behaviorParams; // _params

    public ReleaseBehaviorMsg(AC2Reader data) {
        senderIdWithStamp = data.ReadInstanceIdWithStamp();
        behaviorId = data.ReadEnum<BehaviorId>();
        behaviorParams = new(data);
    }
}
