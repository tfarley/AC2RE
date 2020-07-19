using AC2E.Def;
using System.IO;

namespace AC2E.Def {

    public class QualUpdateBoolPrivateMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdateBool_Private_ID;

        // ECM_Qualities::RecvEvt_UpdateBool_Private
        public uint type; // _stype
        public bool value; // _data

        public QualUpdateBoolPrivateMsg(BinaryReader data) {
            type = data.ReadUInt32();
            value = data.ReadUInt32() != 0;
        }
    }

    public class QualUpdateBoolVisualMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdateBool_Visual_ID;

        // ECM_Qualities::RecvEvt_UpdateBool_Visual
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public uint type; // _stype
        public bool value; // _data

        public QualUpdateBoolVisualMsg(BinaryReader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            type = data.ReadUInt32();
            value = data.ReadUInt32() != 0;
        }
    }
}
