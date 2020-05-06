using AC2E.Def.Structs;
using AC2E.Protocol.NetBlob;
using System.IO;

namespace AC2E.Protocol.Message.Messages {

    public class CPositionMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.WEENIE;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__CPosition_ID;

        public CPositionPack positionPack;

        public CPositionMsg(BinaryReader data) {
            positionPack = new CPositionPack(data);
        }
    }
}
