﻿using System.IO;

namespace AC2E.Protocol {

    public class CLookAtDirMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.WEENIE;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__CLookAtDir_ID;

        // ECM_Physics::SendEvt_CLookAtDir
        public float x; // _x
        public float y; // _y

        public CLookAtDirMsg(BinaryReader data) {
            x = data.ReadSingle();
            y = data.ReadSingle();
        }
    }
}