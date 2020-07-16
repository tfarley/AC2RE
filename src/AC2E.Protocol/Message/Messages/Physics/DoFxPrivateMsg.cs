using System.Collections.Generic;
using System.IO;

namespace AC2E.Protocol {

    public class DoFxPrivateMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__DoFX_Private_ID;

        // ECM_Physics::RecvEvt_DoFX_Private
        public List<uint> fxIds; // _fxIDs

        public DoFxPrivateMsg(BinaryReader data) {
            fxIds = data.ReadList(data.ReadUInt32);
        }
    }
}
