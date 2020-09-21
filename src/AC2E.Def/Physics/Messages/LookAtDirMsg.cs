﻿namespace AC2E.Def {

    public class LookAtDirMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.EPHEMERAL;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__LookAtDir_ID;

        // ECM_Physics::RecvEvt_LookAtDir
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public float z; // _z
        public float x; // _x

        public LookAtDirMsg() {

        }

        public LookAtDirMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            z = data.ReadSingle();
            x = data.ReadSingle();
        }

        public void write(AC2Writer data) {
            data.Write(senderIdWithStamp);
            data.Write(z);
            data.Write(x);
        }
    }
}
