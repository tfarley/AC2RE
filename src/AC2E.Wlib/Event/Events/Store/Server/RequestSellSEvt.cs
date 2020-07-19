using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class RequestSellSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Store__RequestSell;

        // WM_Store::SendSEvt_Store_RequestSell
        public DataId _targetStore;
        public uint _price;
        public uint _quantity;
        public InstanceId _iidItem;
        public InstanceId _iidStorekeeper;

        public RequestSellSEvt(BinaryReader data) {
            _targetStore = data.UnpackDataId();
            _price = data.UnpackUInt32();
            _quantity = data.UnpackUInt32();
            _iidItem = data.UnpackInstanceId();
            _iidStorekeeper = data.UnpackInstanceId();
        }
    }
}
