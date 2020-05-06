using AC2E.Def.Structs;
using AC2E.Protocol.NetBlob;
using System.IO;

namespace AC2E.Protocol.Message.Messages {

    public class PositionCellMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.EPHEMERAL;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__PositionCell_ID;

        public InstanceIdWithStamp idWithStamp;
        public PositionPack positionPack;

        public PositionCellMsg(BinaryReader data) {
            idWithStamp = new InstanceIdWithStamp(data);
            positionPack = new PositionPack(data);
        }
    }
}
