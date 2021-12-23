namespace AC2RE.Definitions;

public class DoSliderMsg : INetMessage {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
    public NetQueue queueId => NetQueue.EVENT;
    public MessageOpcode opcode => MessageOpcode.Physics__DoSlider;
    public OrderingType orderingType => OrderingType.VISUAL_ORDERED;

    // ECM_Physics::RecvEvt_DoSlider
    public InstanceIdWithStamp senderIdWithStamp; // sender
    public uint sliderId; // sliderID
    public float newValue; // _new_value
    public float time; // _time

    public DoSliderMsg() {

    }

    public DoSliderMsg(AC2Reader data) {
        senderIdWithStamp = data.ReadInstanceIdWithStamp();
        sliderId = data.ReadUInt32();
        newValue = data.ReadSingle();
        time = data.ReadSingle();
    }

    public void write(AC2Writer data) {
        data.Write(senderIdWithStamp);
        data.Write(sliderId);
        data.Write(newValue);
        data.Write(time);
    }
}
