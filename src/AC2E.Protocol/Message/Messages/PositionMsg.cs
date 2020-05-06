using AC2E.Def.Structs;
using AC2E.Protocol.NetBlob;
using System.IO;

namespace AC2E.Protocol.Message.Messages {

    public class PositionMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.EPHEMERAL;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__Position_ID;

        public InstanceIdWithStamp idWithStamp;
        public PositionPack positionPack;

        public PositionMsg(BinaryReader data) {
            idWithStamp = new InstanceIdWithStamp(data);
            positionPack = new PositionPack(data);
        }
    }
}
