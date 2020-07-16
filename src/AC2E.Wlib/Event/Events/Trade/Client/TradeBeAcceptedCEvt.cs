using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class TradeBeAcceptedCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Trade__BeAccepted;

        // WM_Trade::PostCEvt_Client_Trade_BeAccepted
        public InstanceId _src;

        public TradeBeAcceptedCEvt() {

        }

        public TradeBeAcceptedCEvt(BinaryReader data) {
            _src = data.UnpackInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_src);
        }
    }
}
