using AC2E.Def;
using System.IO;

namespace AC2E.Protocol {

    public class QualUpdateLongIntPrivateMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdateLongInt_Private_ID;

        // ECM_Qualities::RecvEvt_UpdateLongInt_Visual
        public uint type; // _stype
        public long value; // _data

        public QualUpdateLongIntPrivateMsg(BinaryReader data) {
            type = data.ReadUInt32();
            value = data.ReadInt64();
        }
    }

    public class QualUpdateLongIntVisualMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdateLongInt_Visual_ID;

        // ECM_Qualities::RecvEvt_UpdateLongInt_Visual
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public uint type; // _stype
        public long value; // _data

        public QualUpdateLongIntVisualMsg(BinaryReader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            type = data.ReadUInt32();
            value = data.ReadInt64();
        }
    }
}
