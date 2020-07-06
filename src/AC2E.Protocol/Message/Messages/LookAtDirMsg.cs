using AC2E.Def;
using System.IO;

namespace AC2E.Protocol {

    public class LookAtDirMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.EPHEMERAL;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__LookAtDir_ID;

        // ECM_Physics::RecvEvt_LookAtDir
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public float z; // _z
        public float x; // _x

        public LookAtDirMsg(BinaryReader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            z = data.ReadSingle();
            x = data.ReadSingle();
        }
    }
}
