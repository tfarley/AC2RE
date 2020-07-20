namespace AC2E.Def {

    public class OfferTradeItemSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Trade__OfferTradeItem;

        // WM_Trade::SendSEvt_OfferTradeItem
        public uint _num;
        public InstanceId _item;

        public OfferTradeItemSEvt(AC2Reader data) {
            _num = data.UnpackUInt32();
            _item = data.UnpackInstanceId();
        }
    }
}
