namespace AC2E.Def {

    public class UpdateSaleCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Store__UpdateSale;

        // WM_Store::PostCEvt_Store_UpdateSale
        public int stockDesc; // __iStockdesc
        public InstanceId itemId; // _iidItem
        public InstanceId storekeeperId; // _iidStorekeeper

        public UpdateSaleCEvt() {

        }

        public UpdateSaleCEvt(AC2Reader data) {
            stockDesc = data.UnpackInt32();
            itemId = data.UnpackInstanceId();
            storekeeperId = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(stockDesc);
            data.Pack(itemId);
            data.Pack(storekeeperId);
        }
    }
}
