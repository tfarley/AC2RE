using System.IO;

namespace AC2E.Def {

    public class RequestLeaveCatalogSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Store__RequestLeaveCatalog;

        // WM_Store::SendSEvt_Store_RequestLeaveCatalog
        public InstanceId _iidStorekeeper;

        public RequestLeaveCatalogSEvt(BinaryReader data) {
            _iidStorekeeper = data.UnpackInstanceId();
        }
    }
}
