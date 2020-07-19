using System.IO;

namespace AC2E.Def {

    public class UpdateSaleCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Store__UpdateSale;

        // WM_Store::PostCEvt_Store_UpdateSale
        public int __iStockdesc;
        public InstanceId _iidItem;
        public InstanceId _iidStorekeeper;

        public UpdateSaleCEvt() {

        }

        public UpdateSaleCEvt(BinaryReader data) {
            __iStockdesc = data.UnpackInt32();
            _iidItem = data.UnpackInstanceId();
            _iidStorekeeper = data.UnpackInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Pack(__iStockdesc);
            data.Pack(_iidItem);
            data.Pack(_iidStorekeeper);
        }
    }
}
