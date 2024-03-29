﻿namespace AC2RE.Definitions;

public class QualUpdateTimestampPrivateMsg : INetMessage {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
    public NetQueue queueId => NetQueue.EVENT;
    public MessageOpcode opcode => MessageOpcode.Qualities__UpdateTimestamp_Private;
    public OrderingType orderingType => OrderingType.PRIVATE_ORDERED;

    // ECM_Qualities::RecvEvt_UpdateTimestamp_Private
    public TimestampStat type; // _stype
    public double value; // _data

    public QualUpdateTimestampPrivateMsg(TimestampStat type, double value) {
        this.type = type;
        this.value = value;
    }

    public QualUpdateTimestampPrivateMsg(AC2Reader data) {
        type = data.ReadEnum<TimestampStat>();
        value = data.ReadDouble();
    }

    public void write(AC2Writer data) {
        data.WriteEnum(type);
        data.Write(value);
    }
}

public class QualUpdateTimestampVisualMsg : INetMessage {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
    public NetQueue queueId => NetQueue.EVENT;
    public MessageOpcode opcode => MessageOpcode.Qualities__UpdateTimestamp_Visual;
    public OrderingType orderingType => OrderingType.VISUAL_ORDERED;

    // ECM_Qualities::RecvEvt_UpdateTimestamp_Visual
    public InstanceIdWithStamp senderIdWithStamp; // sender
    public TimestampStat type; // _stype
    public double value; // _data

    public QualUpdateTimestampVisualMsg(InstanceIdWithStamp senderIdWithStamp, TimestampStat type, double value) {
        this.senderIdWithStamp = senderIdWithStamp;
        this.type = type;
        this.value = value;
    }

    public QualUpdateTimestampVisualMsg(AC2Reader data) {
        senderIdWithStamp = data.ReadInstanceIdWithStamp();
        type = data.ReadEnum<TimestampStat>();
        value = data.ReadDouble();
    }

    public void write(AC2Writer data) {
        data.Write(senderIdWithStamp);
        data.WriteEnum(type);
        data.Write(value);
    }
}
