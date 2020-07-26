﻿namespace AC2E.Def {

    public class RequestExecuteCraftSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Craft__RequestExecuteCraft;

        // WM_Craft::SendSEvt_RequestExecuteCraft
        public InstanceId targetId; // _iidTarget
        public ALHash ingredients; // _ingredients
        public int spinnerVal; // _spinnerVal
        public DataId recipeDid; // _didRecipe

        public RequestExecuteCraftSEvt(AC2Reader data) {
            targetId = data.UnpackInstanceId();
            ingredients = data.UnpackPackage<ALHash>();
            spinnerVal = data.UnpackInt32();
            recipeDid = data.UnpackDataId();
        }
    }
}
