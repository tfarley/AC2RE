using System.IO;

namespace AC2E.WLib {

    public class AcceptTradeSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Trade__AcceptTrade;

        // WM_Trade::SendSEvt_AcceptTrade
        public TradePkg _stuff;

        public AcceptTradeSEvt(BinaryReader data) {
            _stuff = data.UnpackPackage<TradePkg>();
        }
    }
}
