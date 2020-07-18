using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class TradeBeRefreshedCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Trade__BeRefreshed;

        // WM_Trade::PostCEvt_Client_Trade_BeRefreshed
        public InstanceId _src;

        public TradeBeRefreshedCEvt() {

        }

        public TradeBeRefreshedCEvt(BinaryReader data) {
            _src = data.UnpackInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_src);
        }
    }
}
