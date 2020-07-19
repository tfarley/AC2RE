using System.IO;

namespace AC2E.Def {

    public class TradeBeOfferedCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Trade__BeOffered;

        // WM_Trade::PostCEvt_Client_Trade_BeOffered
        public uint _amt;
        public InstanceId _item;
        public InstanceId _src;

        public TradeBeOfferedCEvt() {

        }

        public TradeBeOfferedCEvt(BinaryReader data) {
            _amt = data.UnpackUInt32();
            _item = data.UnpackInstanceId();
            _src = data.UnpackInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_amt);
            data.Pack(_item);
            data.Pack(_src);
        }
    }
}
