﻿namespace AC2E.Def {

    public class SetAccelerationScaleMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__SetAccelerationScale_ID;

        // ECM_Physics::RecvEvt_SetAccelerationScale
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public float accelScale; // _value

        public SetAccelerationScaleMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            accelScale = data.ReadSingle();
        }
    }
}
