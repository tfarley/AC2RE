﻿namespace AC2RE.Definitions;

public class LookAtDirMsg : INetMessage {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.EPHEMERAL;
    public NetQueue queueId => NetQueue.EVENT;
    public MessageOpcode opcode => MessageOpcode.Physics__LookAtDir;
    public OrderingType orderingType => OrderingType.UNORDERED;

    // ECM_Physics::RecvEvt_LookAtDir
    public InstanceIdWithStamp senderIdWithStamp; // sender
    public float x; // _x
    public float z; // _z

    public LookAtDirMsg() {

    }

    public LookAtDirMsg(AC2Reader data) {
        senderIdWithStamp = data.ReadInstanceIdWithStamp();
        x = data.ReadSingle();
        z = data.ReadSingle();
    }

    public void write(AC2Writer data) {
        data.Write(senderIdWithStamp);
        data.Write(x);
        data.Write(z);
    }
}
