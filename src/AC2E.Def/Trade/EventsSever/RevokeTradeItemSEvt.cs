namespace AC2E.Def {

    public class RevokeTradeItemSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Trade__RevokeTradeItem;

        // WM_Trade::SendSEvt_RevokeTradeItem
        public InstanceId _item;

        public RevokeTradeItemSEvt(AC2Reader data) {
            _item = data.UnpackInstanceId();
        }
    }
}
