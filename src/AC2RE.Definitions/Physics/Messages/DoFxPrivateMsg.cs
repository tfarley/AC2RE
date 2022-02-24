using System.Collections.Generic;

namespace AC2RE.Definitions;

public class DoFxPrivateMsg : INetMessage {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
    public NetQueue queueId => NetQueue.EVENT;
    public MessageOpcode opcode => MessageOpcode.Physics__DoFX_Private;
    public OrderingType orderingType => OrderingType.PRIVATE_ORDERED;

    // ECM_Physics::RecvEvt_DoFX_Private
    public List<FxId> fxIds; // _fxIDs

    public DoFxPrivateMsg() {

    }

    public DoFxPrivateMsg(AC2Reader data) {
        fxIds = data.ReadList(data.ReadEnum<FxId>);
    }

    public void write(AC2Writer data) {
        data.Write(fxIds, data.WriteEnum);
    }
}
