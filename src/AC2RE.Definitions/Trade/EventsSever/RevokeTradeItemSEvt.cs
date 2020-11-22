namespace AC2RE.Definitions {

    public class RevokeTradeItemSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Trade__RevokeTradeItem;

        // WM_Trade::SendSEvt_RevokeTradeItem
        public InstanceId itemId; // _item

        public RevokeTradeItemSEvt(AC2Reader data) {
            itemId = data.UnpackInstanceId();
        }
    }
}
