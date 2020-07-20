namespace AC2E.Def {

    public class RequestExecuteCraftSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Craft__RequestExecuteCraft;

        // WM_Craft::SendSEvt_RequestExecuteCraft
        public InstanceId _iidTarget;
        public ALHash _ingredients;
        public int _spinnerVal;
        public DataId _didRecipe;

        public RequestExecuteCraftSEvt(AC2Reader data) {
            _iidTarget = data.UnpackInstanceId();
            _ingredients = data.UnpackPackage<ALHash>();
            _spinnerVal = data.UnpackInt32();
            _didRecipe = data.UnpackDataId();
        }
    }
}
