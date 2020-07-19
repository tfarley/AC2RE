using AC2E.Def;
using System.IO;

namespace AC2E.Def {

    public class QualUpdateFloatPrivateMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdateFloat_Private_ID;

        // ECM_Qualities::RecvEvt_UpdateFloat_Private
        public uint type; // _stype
        public float value; // _data

        public QualUpdateFloatPrivateMsg(BinaryReader data) {
            type = data.ReadUInt32();
            value = data.ReadSingle();
        }
    }

    public class QualUpdateFloatVisualMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdateFloat_Visual_ID;

        // ECM_Qualities::RecvEvt_UpdateFloat_Visual
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public uint type; // _stype
        public float value; // _data

        public QualUpdateFloatVisualMsg(BinaryReader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            type = data.ReadUInt32();
            value = data.ReadSingle();
        }
    }
}
