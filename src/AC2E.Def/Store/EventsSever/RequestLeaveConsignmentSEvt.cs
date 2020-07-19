using System.IO;

namespace AC2E.Def {

    public class RequestLeaveConsignmentSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Store__RequestLeaveConsignment;

        // WM_Store::SendSEvt_Store_RequestLeaveConsignment
        public InstanceId _iidStorekeeper;

        public RequestLeaveConsignmentSEvt(BinaryReader data) {
            _iidStorekeeper = data.UnpackInstanceId();
        }
    }
}
