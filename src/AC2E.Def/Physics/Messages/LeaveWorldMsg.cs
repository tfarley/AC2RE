using AC2E.Def;
using AC2E.Utils;
using System.IO;

namespace AC2E.Def {

    public class LeaveWorldMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.EPHEMERAL;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__LeaveWorld_ID;

        // ECM_Physics::RecvEvt_LeaveWorld
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public ushort positionStamp; // _position_stamp

        public LeaveWorldMsg(BinaryReader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            positionStamp = data.ReadUInt16();
            data.Align(4);
        }
    }
}
