using AC2E.Def;
using System.IO;

namespace AC2E.Def {

    public class DestroyObjectMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__DestroyObject_ID;

        // ECM_Physics::RecvEvt_DestroyObject
        public InstanceIdWithStamp objectIdWithStamp; // _object

        public DestroyObjectMsg() {

        }

        public DestroyObjectMsg(BinaryReader data) {
            objectIdWithStamp = data.ReadInstanceIdWithStamp();
        }

        public void write(BinaryWriter data) {
            data.Write(objectIdWithStamp);
        }
    }
}
