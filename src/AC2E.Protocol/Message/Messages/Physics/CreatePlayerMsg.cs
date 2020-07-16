using AC2E.Def;
using System.IO;

namespace AC2E.Protocol {

    public class CreatePlayerMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__CreatePlayer_ID;

        // ECM_Physics::RecvEvt_CreatePlayer
        public InstanceId objectId; // _objectID
        public uint regionId; // _regionID

        public CreatePlayerMsg() {

        }

        public CreatePlayerMsg(BinaryReader data) {
            objectId = data.ReadInstanceId();
            regionId = data.ReadUInt32();
        }

        public void write(BinaryWriter data) {
            data.Write(objectId);
            data.Write(regionId);
        }
    }
}
