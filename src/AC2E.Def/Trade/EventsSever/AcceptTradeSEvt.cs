using System.IO;

namespace AC2E.Def {

    public class AcceptTradeSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Trade__AcceptTrade;

        // WM_Trade::SendSEvt_AcceptTrade
        public Trade _stuff;

        public AcceptTradeSEvt(BinaryReader data) {
            _stuff = data.UnpackPackage<Trade>();
        }
    }
}
