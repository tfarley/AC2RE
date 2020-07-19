using AC2E.Def;
using System.IO;

namespace AC2E.Def {

    public class SetJumpScaleMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__SetJumpScale_ID;

        // ECM_Physics::RecvEvt_SetJumpScale
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public float value; // _value

        public SetJumpScaleMsg(BinaryReader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            value = data.ReadSingle();
        }
    }
}
