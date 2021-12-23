namespace AC2RE.Definitions;

public class ContainMsg : INetMessage {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
    public NetQueue queueId => NetQueue.EVENT;
    public MessageOpcode opcode => MessageOpcode.Physics__Contain;

    // ECM_Physics::RecvEvt_Contain
    public InstanceIdWithStamp childIdWithPosStamp; // _child_id_and_position_stamp

    public ContainMsg() {

    }

    public ContainMsg(AC2Reader data) {
        childIdWithPosStamp = data.ReadInstanceIdWithStamp();
    }

    public void write(AC2Writer data) {
        data.Write(childIdWithPosStamp);
    }
}
