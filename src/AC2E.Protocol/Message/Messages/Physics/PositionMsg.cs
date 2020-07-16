using AC2E.Def;
using System.IO;

namespace AC2E.Protocol {

    public class PositionMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.EPHEMERAL;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__Position_ID;

        // ECM_Physics::RecvEvt_Position
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public PositionPack positionPack; // _position_pack

        public PositionMsg(BinaryReader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            positionPack = new PositionPack(data);
        }
    }
}
