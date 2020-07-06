﻿using AC2E.Def;
using System.IO;

namespace AC2E.Protocol {

    public class QualUpdateTimestampPrivateMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdateTimestamp_Private_ID;

        // ECM_Qualities::RecvEvt_UpdateTimestamp_Visual
        public uint type; // _stype
        public double value; // _data

        public QualUpdateTimestampPrivateMsg(BinaryReader data) {
            type = data.ReadUInt32();
            value = data.ReadDouble();
        }
    }

    public class QualUpdateTimestampVisualMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdateTimestamp_Visual_ID;

        // ECM_Qualities::RecvEvt_UpdateTimestamp_Visual
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public uint type; // _stype
        public double value; // _data

        public QualUpdateTimestampVisualMsg(BinaryReader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            type = data.ReadUInt32();
            value = data.ReadDouble();
        }
    }
}
