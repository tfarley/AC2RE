namespace AC2RE.Definitions {

    public class TryToUseItemSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Usage__TryToUseItem;

        // WM_Usage::SendSEvt_Usage_TryToUseItem
        public UsageDesc usageDesc; // _uDesc

        public TryToUseItemSEvt(AC2Reader data) {
            usageDesc = data.UnpackPackage<UsageDesc>();
        }
    }
}
