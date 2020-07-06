using AC2E.Def;
using System.IO;

namespace AC2E.Protocol {

    public class QualUpdateStringInfoPrivateMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdateStringInfo_Private_ID;

        // ECM_Qualities::RecvEvt_UpdateStringInfo_Visual
        public uint type; // _stype
        public StringInfo value; // _data

        public QualUpdateStringInfoPrivateMsg(BinaryReader data) {
            type = data.ReadUInt32();
            value = new StringInfo(data);
        }
    }

    public class QualUpdateStringInfoVisualMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdateStringInfo_Visual_ID;

        // ECM_Qualities::RecvEvt_UpdateStringInfo_Visual
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public uint type; // _stype
        public StringInfo value; // _data

        public QualUpdateStringInfoVisualMsg(BinaryReader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            type = data.ReadUInt32();
            value = new StringInfo(data);
        }
    }
}
