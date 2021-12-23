namespace AC2RE.Definitions;

public class DoModeMsg : INetMessage {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
    public NetQueue queueId => NetQueue.EVENT;
    public MessageOpcode opcode => MessageOpcode.Physics__DoMode;
    public OrderingType orderingType => OrderingType.VISUAL_ORDERED;

    // ECM_Physics::RecvEvt_DoMode
    public InstanceIdWithStamp senderIdWithStamp; // sender
    public ModeId modeId; // mode_id

    public DoModeMsg() {

    }

    public DoModeMsg(AC2Reader data) {
        senderIdWithStamp = data.ReadInstanceIdWithStamp();
        modeId = (ModeId)data.ReadUInt32();
    }

    public void write(AC2Writer data) {
        data.Write(senderIdWithStamp);
        data.Write((uint)modeId);
    }
}
