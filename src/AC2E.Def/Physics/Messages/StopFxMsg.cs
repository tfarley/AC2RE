﻿namespace AC2E.Def {

    public class StopFxMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__StopFX_ID;

        // ECM_Physics::RecvEvt_StopFX
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public uint fxId; // _fx_id

        public StopFxMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            fxId = data.ReadUInt32();
        }
    }
}