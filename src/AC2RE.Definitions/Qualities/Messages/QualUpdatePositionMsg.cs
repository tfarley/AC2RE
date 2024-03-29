﻿namespace AC2RE.Definitions;

public class QualUpdatePositionPrivateMsg : INetMessage {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
    public NetQueue queueId => NetQueue.EVENT;
    public MessageOpcode opcode => MessageOpcode.Qualities__UpdatePosition_Private;
    public OrderingType orderingType => OrderingType.PRIVATE_ORDERED;

    // ECM_Qualities::RecvEvt_UpdatePosition_Private
    public PositionStat type; // _stype
    public Position value; // _data

    public QualUpdatePositionPrivateMsg(PositionStat type, Position value) {
        this.type = type;
        this.value = value;
    }

    public QualUpdatePositionPrivateMsg(AC2Reader data) {
        type = data.ReadEnum<PositionStat>();
        value = new(data);
    }

    public void write(AC2Writer data) {
        data.WriteEnum(type);
        value.write(data);
    }
}

public class QualUpdatePositionVisualMsg : INetMessage {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
    public NetQueue queueId => NetQueue.EVENT;
    public MessageOpcode opcode => MessageOpcode.Qualities__UpdatePosition_Visual;
    public OrderingType orderingType => OrderingType.VISUAL_ORDERED;

    // ECM_Qualities::RecvEvt_UpdatePosition_Visual
    public InstanceIdWithStamp senderIdWithStamp; // sender
    public PositionStat type; // _stype
    public Position value; // _data

    public QualUpdatePositionVisualMsg(InstanceIdWithStamp senderIdWithStamp, PositionStat type, Position value) {
        this.senderIdWithStamp = senderIdWithStamp;
        this.type = type;
        this.value = value;
    }

    public QualUpdatePositionVisualMsg(AC2Reader data) {
        senderIdWithStamp = data.ReadInstanceIdWithStamp();
        type = data.ReadEnum<PositionStat>();
        value = new(data);
    }

    public void write(AC2Writer data) {
        data.Write(senderIdWithStamp);
        data.WriteEnum(type);
        value.write(data);
    }
}
