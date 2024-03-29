﻿namespace AC2RE.Definitions;

public class CForceCreateMsg : INetMessage {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
    public NetQueue queueId => NetQueue.WEENIE;
    public MessageOpcode opcode => MessageOpcode.Physics__CForceCreate;
    public OrderingType orderingType => OrderingType.PRIVATE_ORDERED;

    // ECM_Physics::SendEvt_CForceCreate
    public InstanceId id; // _object_id

    public CForceCreateMsg(AC2Reader data) {
        id = data.ReadInstanceId();
    }
}
