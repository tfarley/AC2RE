namespace AC2RE.Definitions;

public class QualUpdateIntPrivateMsg : INetMessage {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
    public NetQueue queueId => NetQueue.EVENT;
    public MessageOpcode opcode => MessageOpcode.Qualities__UpdateInt_Private;

    // ECM_Qualities::RecvEvt_UpdateInt_Private
    public IntStat type; // _stype
    public int value; // _data

    public QualUpdateIntPrivateMsg(IntStat type, int value) {
        this.type = type;
        this.value = value;
    }

    public QualUpdateIntPrivateMsg(AC2Reader data) {
        type = (IntStat)data.ReadUInt32();
        value = data.ReadInt32();
    }

    public void write(AC2Writer data) {
        data.Write((uint)type);
        data.Write(value);
    }
}

public class QualUpdateIntVisualMsg : INetMessage {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
    public NetQueue queueId => NetQueue.EVENT;
    public MessageOpcode opcode => MessageOpcode.Qualities__UpdateInt_Visual;
    public OrderingType orderingType => OrderingType.VISUAL_ORDERED;

    // ECM_Qualities::RecvEvt_UpdateInt_Visual
    public InstanceIdWithStamp senderIdWithStamp; // sender
    public IntStat type; // _stype
    public int value; // _data

    public QualUpdateIntVisualMsg(InstanceIdWithStamp senderIdWithStamp, IntStat type, int value) {
        this.senderIdWithStamp = senderIdWithStamp;
        this.type = type;
        this.value = value;
    }

    public QualUpdateIntVisualMsg(AC2Reader data) {
        senderIdWithStamp = data.ReadInstanceIdWithStamp();
        type = (IntStat)data.ReadUInt32();
        value = data.ReadInt32();
    }

    public void write(AC2Writer data) {
        data.Write(senderIdWithStamp);
        data.Write((uint)type);
        data.Write(value);
    }
}
