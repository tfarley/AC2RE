using AC2E.Def;
using System.IO;

namespace AC2E.Def {

    public class DoFxMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__DoFX_ID;

        // ECM_Physics::RecvEvt_DoFX
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public uint fxId; // _fx_id
        public float scalar; // __scalar

        public DoFxMsg(BinaryReader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            fxId = data.ReadUInt32();
            scalar = data.ReadSingle();
        }
    }
}
