using AC2E.Def;
using System.IO;

namespace AC2E.Protocol {

    public class PositionCellMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.EPHEMERAL;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__PositionCell_ID;

        // ECM_Physics::RecvEvt_PositionCell
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public PositionPack positionPack; // __pp

        public PositionCellMsg(BinaryReader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            positionPack = new PositionPack(data);
        }
    }
}
