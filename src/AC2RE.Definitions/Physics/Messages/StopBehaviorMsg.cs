namespace AC2RE.Definitions;

public class StopBehaviorMsg : INetMessage {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
    public NetQueue queueId => NetQueue.EVENT;
    public MessageOpcode opcode => MessageOpcode.Physics__StopBehavior;
    public OrderingType orderingType => OrderingType.VISUAL_ORDERED;

    // ECM_Physics::RecvEvt_StopBehavior
    public InstanceIdWithStamp senderIdWithStamp; // sender
    public BehaviorId behaviorId; // bvrID

    public StopBehaviorMsg() {

    }

    public StopBehaviorMsg(AC2Reader data) {
        senderIdWithStamp = data.ReadInstanceIdWithStamp();
        behaviorId = (BehaviorId)data.ReadUInt32();
    }

    public void write(AC2Writer data) {
        data.Write(senderIdWithStamp);
        data.Write((uint)behaviorId);
    }
}
