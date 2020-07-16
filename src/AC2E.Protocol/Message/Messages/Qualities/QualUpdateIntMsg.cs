using AC2E.Def;
using System.IO;

namespace AC2E.Protocol {

    public class QualUpdateIntPrivateMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdateInt_Private_ID;

        // ECM_Qualities::RecvEvt_UpdateInt_Private
        public uint type; // _stype
        public int value; // _data

        public QualUpdateIntPrivateMsg(BinaryReader data) {
            type = data.ReadUInt32();
            value = data.ReadInt32();
        }
    }

    public class QualUpdateIntVisualMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdateInt_Visual_ID;

        // ECM_Qualities::RecvEvt_UpdateInt_Visual
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public uint type; // _stype
        public int value; // _data

        public QualUpdateIntVisualMsg(BinaryReader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            type = data.ReadUInt32();
            value = data.ReadInt32();
        }
    }
}
