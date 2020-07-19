using System.IO;

namespace AC2E.Def {

    public class RequestEnterConsignmentSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Store__RequestEnterConsignment;

        // WM_Store::SendSEvt_Store_RequestEnterConsignment
        public InstanceId _iidStorekeeper;

        public RequestEnterConsignmentSEvt(BinaryReader data) {
            _iidStorekeeper = data.UnpackInstanceId();
        }
    }
}
