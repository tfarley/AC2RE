namespace AC2RE.Definitions;

public class OfferTradeItemSEvt : IServerEvent {

    public ServerEventFunctionId funcId => ServerEventFunctionId.Trade__OfferTradeItem;

    // WM_Trade::SendSEvt_OfferTradeItem
    public uint amount; // _num
    public InstanceId itemId; // _item

    public OfferTradeItemSEvt(AC2Reader data) {
        amount = data.UnpackUInt32();
        itemId = data.UnpackInstanceId();
    }
}
