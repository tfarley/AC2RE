namespace AC2RE.Definitions {

    public class TryToUseItemDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Usage__TryToUseItem_Done;

        // WM_Usage::PostCEvt_Usage_TryToUseItem_Done
        public UsageDesc usageDesc; // _uDesc

        public TryToUseItemDoneCEvt() {

        }

        public TryToUseItemDoneCEvt(AC2Reader data) {
            usageDesc = data.UnpackPackage<UsageDesc>();
        }

        public void write(AC2Writer data) {
            data.Pack(usageDesc);
        }
    }
}
