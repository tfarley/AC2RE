using System.IO;

namespace AC2E.Def {

    public class TradeBeFailedCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Trade__BeFailed;

        // WM_Trade::PostCEvt_Client_Trade_BeFailed
        public uint _etype;

        public TradeBeFailedCEvt() {

        }

        public TradeBeFailedCEvt(BinaryReader data) {
            _etype = data.UnpackUInt32();
        }

        public void write(BinaryWriter data) {
            data.Pack(_etype);
        }
    }
}
