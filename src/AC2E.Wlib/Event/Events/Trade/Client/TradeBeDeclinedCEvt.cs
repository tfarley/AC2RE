using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class TradeBeDeclinedCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Trade__BeDeclined;

        // WM_Trade::PostCEvt_Client_Trade_BeDeclined
        public InstanceId _src;

        public TradeBeDeclinedCEvt() {

        }

        public TradeBeDeclinedCEvt(BinaryReader data) {
            _src = data.UnpackInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_src);
        }
    }
}
