namespace AC2E.Def {

    public class StoreRequestDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Store__Request_Done;

        // WM_Store::PostCEvt_Store_Request_Done
        public uint _err;

        public StoreRequestDoneCEvt() {

        }

        public StoreRequestDoneCEvt(AC2Reader data) {
            _err = data.UnpackUInt32();
        }

        public void write(AC2Writer data) {
            data.Pack(_err);
        }
    }
}
