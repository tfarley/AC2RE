﻿using AC2E.Def;
using System.IO;

namespace AC2E.Protocol {

    public class QualUpdateInstanceIdPrivateMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdateInstanceID_Private_ID;

        // ECM_Qualities::RecvEvt_UpdateInstanceID_Private
        public uint type; // _stype
        public InstanceId value; // _data

        public QualUpdateInstanceIdPrivateMsg(BinaryReader data) {
            type = data.ReadUInt32();
            value = data.ReadInstanceId();
        }
    }

    public class QualUpdateInstanceIdVisualMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdateInstanceID_Visual_ID;

        // ECM_Qualities::RecvEvt_UpdateInstanceID_Visual
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public uint type; // _stype
        public InstanceId value; // _data

        public QualUpdateInstanceIdVisualMsg(BinaryReader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            type = data.ReadUInt32();
            value = data.ReadInstanceId();
        }
    }
}