using AC2E.Def;
using System.IO;

namespace AC2E.Protocol {

    public class QualUpdatePositionPrivateMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdatePosition_Private_ID;

        // ECM_Qualities::RecvEvt_UpdatePosition_Private
        public uint type; // _stype
        public Position value; // _data

        public QualUpdatePositionPrivateMsg(BinaryReader data) {
            type = data.ReadUInt32();
            value = new Position(data);
        }
    }

    public class QualUpdatePositionVisualMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdatePosition_Visual_ID;

        // ECM_Qualities::RecvEvt_UpdatePosition_Visual
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public uint type; // _stype
        public Position value; // _data

        public QualUpdatePositionVisualMsg(BinaryReader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            type = data.ReadUInt32();
            value = new Position(data);
        }
    }
}
