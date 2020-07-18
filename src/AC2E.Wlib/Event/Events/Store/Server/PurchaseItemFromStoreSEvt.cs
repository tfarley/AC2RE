using System.IO;

namespace AC2E.WLib {

    public class PurchaseItemFromStoreSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Store__PurchaseItemFromStore;

        // WM_Store::SendSEvt_Store_PurchaseItemFromStore
        public TransactionBlobPkg _blob;

        public PurchaseItemFromStoreSEvt(BinaryReader data) {
            _blob = data.UnpackPackage<TransactionBlobPkg>();
        }
    }
}
