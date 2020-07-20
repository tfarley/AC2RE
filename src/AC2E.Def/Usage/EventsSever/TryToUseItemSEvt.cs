namespace AC2E.Def {

    public class TryToUseItemSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Usage__Usage_TryToUseItem;

        // WM_Usage::SendSEvt_Usage_TryToUseItem
        public UsageDesc _uDesc;

        public TryToUseItemSEvt(AC2Reader data) {
            _uDesc = data.UnpackPackage<UsageDesc>();
        }
    }
}
