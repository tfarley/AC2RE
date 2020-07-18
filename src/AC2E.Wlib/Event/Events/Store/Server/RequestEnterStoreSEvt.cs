using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class RequestEnterStoreSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Store__RequestEnterStore;

        // WM_Store::SendSEvt_Store_RequestEnterStore
        public DataId _didStore;
        public InstanceId _iidStorekeeper;

        public RequestEnterStoreSEvt(BinaryReader data) {
            _didStore = data.UnpackDataId();
            _iidStorekeeper = data.UnpackInstanceId();
        }
    }
}
