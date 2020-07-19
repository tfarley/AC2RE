using System.IO;

namespace AC2E.Def {

    public class RevokeTradeItemSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Trade__RevokeTradeItem;

        // WM_Trade::SendSEvt_RevokeTradeItem
        public InstanceId _item;

        public RevokeTradeItemSEvt(BinaryReader data) {
            _item = data.UnpackInstanceId();
        }
    }
}
