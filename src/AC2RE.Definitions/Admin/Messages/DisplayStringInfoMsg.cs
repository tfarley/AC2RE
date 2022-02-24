namespace AC2RE.Definitions;

public class DisplayStringInfoMsg : INetMessage {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
    public NetQueue queueId => NetQueue.EVENT;
    public MessageOpcode opcode => MessageOpcode.Admin__DisplayStringInfo;
    public OrderingType orderingType => OrderingType.UNORDERED;

    // ECM_Admin::RecvEvt_DisplayStringInfo
    public TextType type; // type
    public StringInfo text; // _si

    public DisplayStringInfoMsg() {

    }

    public DisplayStringInfoMsg(AC2Reader data) {
        type = data.ReadEnum<TextType>();
        text = new(data);
    }

    public void write(AC2Writer data) {
        data.WriteEnum(type);
        text.write(data);
    }
}
