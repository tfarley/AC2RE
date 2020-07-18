using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class TradeBeRevokedCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Trade__BeRevoked;

        // WM_Trade::PostCEvt_Client_Trade_BeRevoked
        public InstanceId _item;
        public InstanceId _src;

        public TradeBeRevokedCEvt() {

        }

        public TradeBeRevokedCEvt(BinaryReader data) {
            _item = data.UnpackInstanceId();
            _src = data.UnpackInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_item);
            data.Pack(_src);
        }
    }
}
