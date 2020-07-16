using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class RequestNextSalesSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Store__RequestNextSales;

        // WM_Store::SendSEvt_Store_RequestNextSales
        public InstanceId _iidStorekeeper;

        public RequestNextSalesSEvt(BinaryReader data) {
            _iidStorekeeper = data.UnpackInstanceId();
        }
    }
}
