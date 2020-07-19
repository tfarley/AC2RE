﻿using AC2E.Def;
using System.IO;

namespace AC2E.Def {

    public class DoBehaviorMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__DoBehavior_ID;

        // ECM_Physics::RecvEvt_DoBehavior
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public BehaviorParams behaviorParams; // _params

        public DoBehaviorMsg(BinaryReader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            behaviorParams = new BehaviorParams(data);
        }
    }
}
