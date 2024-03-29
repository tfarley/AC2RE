﻿namespace AC2RE.Definitions;

public class StopFxMsg : INetMessage {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
    public NetQueue queueId => NetQueue.EVENT;
    public MessageOpcode opcode => MessageOpcode.Physics__StopFX;
    public OrderingType orderingType => OrderingType.VISUAL_ORDERED;

    // ECM_Physics::RecvEvt_StopFX
    public InstanceIdWithStamp senderIdWithStamp; // sender
    public FxId fxId; // _fx_id

    public StopFxMsg() {

    }

    public StopFxMsg(AC2Reader data) {
        senderIdWithStamp = data.ReadInstanceIdWithStamp();
        fxId = data.ReadEnum<FxId>();
    }

    public void write(AC2Writer data) {
        data.Write(senderIdWithStamp);
        data.WriteEnum(fxId);
    }
}
