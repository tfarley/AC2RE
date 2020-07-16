using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class RequestPrevSalesSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Store__RequestPrevSales;

        // WM_Store::SendSEvt_Store_RequestPrevSales
        public InstanceId _iidStorekeeper;

        public RequestPrevSalesSEvt(BinaryReader data) {
            _iidStorekeeper = data.UnpackInstanceId();
        }
    }
}
