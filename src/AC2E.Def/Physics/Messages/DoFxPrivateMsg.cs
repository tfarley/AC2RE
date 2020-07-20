using System.Collections.Generic;

namespace AC2E.Def {

    public class DoFxPrivateMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__DoFX_Private_ID;

        // ECM_Physics::RecvEvt_DoFX_Private
        public List<uint> fxIds; // _fxIDs

        public DoFxPrivateMsg(AC2Reader data) {
            fxIds = data.ReadList(data.ReadUInt32);
        }
    }
}
