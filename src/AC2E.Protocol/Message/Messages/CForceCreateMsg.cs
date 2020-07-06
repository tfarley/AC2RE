using AC2E.Def;
using System.IO;

namespace AC2E.Protocol {

    public class CForceCreateMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.WEENIE;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__CForceCreate_ID;

        // ECM_Physics::SendEvt_CForceCreate
        public InstanceId objectId; // _object_id

        public CForceCreateMsg(BinaryReader data) {
            objectId = data.ReadInstanceId();
        }
    }
}
