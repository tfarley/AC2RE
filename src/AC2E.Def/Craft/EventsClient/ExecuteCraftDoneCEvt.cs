namespace AC2E.Def {

    public class ExecuteCraftDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Craft__ExecuteCraft_Done;

        // WM_Craft::PostCEvt_ExecuteCraft_Done
        public bool notifyUI; // _bNotifyUI
        public ErrorType error; // _err
        public DataId recipeDid; // _didRecipe

        public ExecuteCraftDoneCEvt() {

        }

        public ExecuteCraftDoneCEvt(AC2Reader data) {
            notifyUI = data.UnpackBoolean();
            error = (ErrorType)data.UnpackUInt32();
            recipeDid = data.UnpackDataId();
        }

        public void write(AC2Writer data) {
            data.Pack(notifyUI);
            data.Pack((uint)error);
            data.Pack(recipeDid);
        }
    }
}
