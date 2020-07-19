using System.IO;

namespace AC2E.Def {

    public class PurchaseItemFromStoreSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Store__PurchaseItemFromStore;

        // WM_Store::SendSEvt_Store_PurchaseItemFromStore
        public TransactionBlob _blob;

        public PurchaseItemFromStoreSEvt(BinaryReader data) {
            _blob = data.UnpackPackage<TransactionBlob>();
        }
    }
}
