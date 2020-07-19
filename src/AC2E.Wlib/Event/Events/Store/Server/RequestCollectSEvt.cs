using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class RequestCollectSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Store__RequestCollect;

        // WM_Store::SendSEvt_Store_RequestCollect
        public bool _bProfits;
        public uint _target;
        public InstanceId _iidStorekeeper;

        public RequestCollectSEvt(BinaryReader data) {
            _bProfits = data.UnpackUInt32() != 0;
            _target = data.UnpackUInt32();
            _iidStorekeeper = data.UnpackInstanceId();
        }
    }
}
