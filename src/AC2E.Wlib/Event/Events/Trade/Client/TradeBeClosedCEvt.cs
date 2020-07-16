using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class TradeBeClosedCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Trade__BeClosed;

        // WM_Trade::PostCEvt_Client_Trade_BeClosed
        public uint _etype;
        public InstanceId _src;

        public TradeBeClosedCEvt() {

        }

        public TradeBeClosedCEvt(BinaryReader data) {
            _etype = data.UnpackUInt32();
            _src = data.UnpackInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_etype);
            data.Pack(_src);
        }
    }
}
