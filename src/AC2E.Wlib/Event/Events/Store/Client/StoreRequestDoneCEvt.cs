using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class StoreRequestDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Store__Request_Done;

        // WM_Store::PostCEvt_Store_Request_Done
        public uint _err;

        public StoreRequestDoneCEvt() {

        }

        public StoreRequestDoneCEvt(BinaryReader data) {
            _err = data.UnpackUInt32();
        }

        public void write(BinaryWriter data) {
            data.Pack(_err);
        }
    }
}
