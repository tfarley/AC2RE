namespace AC2RE.Definitions;

public class QualUpdateDataIdPrivateMsg : INetMessage {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
    public NetQueue queueId => NetQueue.EVENT;
    public MessageOpcode opcode => MessageOpcode.Qualities__UpdateDataID_Private;
    public OrderingType orderingType => OrderingType.PRIVATE_ORDERED;

    // ECM_Qualities::RecvEvt_UpdateDataID_Private
    public DataIdStat type; // _stype
    public DataId value; // _data

    public QualUpdateDataIdPrivateMsg(DataIdStat type, DataId value) {
        this.type = type;
        this.value = value;
    }

    public QualUpdateDataIdPrivateMsg(AC2Reader data) {
        type = data.ReadEnum<DataIdStat>();
        value = data.ReadDataId();
    }

    public void write(AC2Writer data) {
        data.WriteEnum(type);
        data.Write(value);
    }
}

public class QualUpdateDataIdVisualMsg : INetMessage {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
    public NetQueue queueId => NetQueue.EVENT;
    public MessageOpcode opcode => MessageOpcode.Qualities__UpdateDataID_Visual;
    public OrderingType orderingType => OrderingType.VISUAL_ORDERED;

    // ECM_Qualities::RecvEvt_UpdateDataID_Visual
    public InstanceIdWithStamp senderIdWithStamp; // sender
    public DataIdStat type; // _stype
    public DataId value; // _data

    public QualUpdateDataIdVisualMsg(InstanceIdWithStamp senderIdWithStamp, DataIdStat type, DataId value) {
        this.senderIdWithStamp = senderIdWithStamp;
        this.type = type;
        this.value = value;
    }

    public QualUpdateDataIdVisualMsg(AC2Reader data) {
        senderIdWithStamp = data.ReadInstanceIdWithStamp();
        type = data.ReadEnum<DataIdStat>();
        value = data.ReadDataId();
    }

    public void write(AC2Writer data) {
        data.Write(senderIdWithStamp);
        data.WriteEnum(type);
        data.Write(value);
    }
}
