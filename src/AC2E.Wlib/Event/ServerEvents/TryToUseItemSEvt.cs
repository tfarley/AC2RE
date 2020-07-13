using System.IO;

namespace AC2E.WLib {

    public class TryToUseItemSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Usage__Usage_TryToUseItem;

        // WM_Usage::SendSEvt_Usage_TryToUseItem
        public UsageDescPkg _uDesc;

        public TryToUseItemSEvt() {

        }

        public TryToUseItemSEvt(BinaryReader data) {
            _uDesc = data.UnpackPackage<UsageDescPkg>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_uDesc);
        }
    }
}
