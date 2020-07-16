using AC2E.Def;
using System.IO;

namespace AC2E.Protocol {

    public class PlayerDescMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Login__PlayerDesc_ID;

        // ECM_Login::RecvEvt_PlayerDesc
        public CBaseQualities baseQualities; // _q

        public PlayerDescMsg() {

        }

        public PlayerDescMsg(BinaryReader data) {
            baseQualities = new CBaseQualities(data);
        }

        public void write(BinaryWriter data) {
            baseQualities.write(data);
        }
    }
}
