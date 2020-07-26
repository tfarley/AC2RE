﻿namespace AC2E.Def {

    public class DestroyObjectMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__DestroyObject_ID;

        // ECM_Physics::RecvEvt_DestroyObject
        public InstanceIdWithStamp idWithStamp; // _object

        public DestroyObjectMsg() {

        }

        public DestroyObjectMsg(AC2Reader data) {
            idWithStamp = data.ReadInstanceIdWithStamp();
        }

        public void write(AC2Writer data) {
            data.Write(idWithStamp);
        }
    }
}
