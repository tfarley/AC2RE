namespace AC2RE.Definitions;

public class QualUpdateBoolPrivateMsg : INetMessage {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
    public NetQueue queueId => NetQueue.EVENT;
    public MessageOpcode opcode => MessageOpcode.Qualities__UpdateBool_Private;

    // ECM_Qualities::RecvEvt_UpdateBool_Private
    public BoolStat type; // _stype
    public bool value; // _data

    public QualUpdateBoolPrivateMsg(BoolStat type, bool value) {
        this.type = type;
        this.value = value;
    }

    public QualUpdateBoolPrivateMsg(AC2Reader data) {
        type = data.ReadEnum<BoolStat>();
        value = data.ReadBoolean();
    }

    public void write(AC2Writer data) {
        data.WriteEnum(type);
        data.Write(value);
    }
}

public class QualUpdateBoolVisualMsg : INetMessage {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
    public NetQueue queueId => NetQueue.EVENT;
    public MessageOpcode opcode => MessageOpcode.Qualities__UpdateBool_Visual;
    public OrderingType orderingType => OrderingType.VISUAL_ORDERED;

    // ECM_Qualities::RecvEvt_UpdateBool_Visual
    public InstanceIdWithStamp senderIdWithStamp; // sender
    public BoolStat type; // _stype
    public bool value; // _data

    public QualUpdateBoolVisualMsg(InstanceIdWithStamp senderIdWithStamp, BoolStat type, bool value) {
        this.senderIdWithStamp = senderIdWithStamp;
        this.type = type;
        this.value = value;
    }

    public QualUpdateBoolVisualMsg(AC2Reader data) {
        senderIdWithStamp = data.ReadInstanceIdWithStamp();
        type = data.ReadEnum<BoolStat>();
        value = data.ReadBoolean();
    }

    public void write(AC2Writer data) {
        data.Write(senderIdWithStamp);
        data.WriteEnum(type);
        data.Write(value);
    }
}
