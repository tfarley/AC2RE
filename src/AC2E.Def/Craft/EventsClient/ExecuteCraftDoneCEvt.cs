namespace AC2E.Def {

    public class ExecuteCraftDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Craft__ExecuteCraft_Done;

        // WM_Craft::PostCEvt_ExecuteCraft_Done
        public bool _bNotifyUI;
        public uint _err;
        public DataId _didRecipe;

        public ExecuteCraftDoneCEvt() {

        }

        public ExecuteCraftDoneCEvt(AC2Reader data) {
            _bNotifyUI = data.UnpackBoolean();
            _err = data.UnpackUInt32();
            _didRecipe = data.UnpackDataId();
        }

        public void write(AC2Writer data) {
            data.Pack(_bNotifyUI);
            data.Pack(_err);
            data.Pack(_didRecipe);
        }
    }
}
