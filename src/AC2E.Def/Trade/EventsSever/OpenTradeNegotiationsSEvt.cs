using System.IO;

namespace AC2E.Def {

    public class OpenTradeNegotiationsSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Trade__OpenTradeNegotiations;

        // WM_Trade::SendSEvt_OpenTradeNegotiations
        public InstanceId _trg;

        public OpenTradeNegotiationsSEvt(BinaryReader data) {
            _trg = data.UnpackInstanceId();
        }
    }
}
