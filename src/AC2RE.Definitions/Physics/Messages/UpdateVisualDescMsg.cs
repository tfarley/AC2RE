namespace AC2RE.Definitions;

public class UpdateVisualDescMsg : INetMessage {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
    public NetQueue queueId => NetQueue.EVENT;
    public MessageOpcode opcode => MessageOpcode.Physics__UpdateVisualDesc;
    public OrderingType orderingType => OrderingType.VISUAL_ORDERED;

    // ECM_Physics::RecvEvt_UpdateVisualDesc
    public InstanceIdWithStamp senderIdWithStamp; // sender
    public VisualDesc visualDesc; // _vdesc

    public UpdateVisualDescMsg(InstanceIdWithStamp senderIdWithStamp, VisualDesc visualDesc) {
        this.senderIdWithStamp = senderIdWithStamp;
        this.visualDesc = visualDesc;
    }

    public UpdateVisualDescMsg(AC2Reader data) {
        senderIdWithStamp = data.ReadInstanceIdWithStamp();
        visualDesc = new(data);
    }

    public void write(AC2Writer data) {
        data.Write(senderIdWithStamp);
        visualDesc.write(data);
    }
}
