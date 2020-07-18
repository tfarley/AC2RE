using AC2E.Def;
using AC2E.Utils;
using System.IO;

namespace AC2E.Protocol {

    public class MoveToMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__MoveTo_ID;

        // ECM_Physics::RecvEvt_MoveTo
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public MovementParameters movementParameters; // _params
        public ushort movetoStamp; // _moveto_stamp

        public MoveToMsg(BinaryReader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            movementParameters = new MovementParameters(data);
            movetoStamp = data.ReadUInt16();
            data.Align(4);
        }
    }
}
