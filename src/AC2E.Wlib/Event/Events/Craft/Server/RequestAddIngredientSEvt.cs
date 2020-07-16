﻿using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class RequestAddIngredientSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Craft__RequestAddIngredient;

        // WM_Craft::SendSEvt_RequestAddIngredient
        public uint _uiSpinnerVal;
        public InstanceId _iidIngredient;
        public uint _uiOrdinal;
        public DataId _didRecipe;

        public RequestAddIngredientSEvt(BinaryReader data) {
            _uiSpinnerVal = data.UnpackUInt32();
            _iidIngredient = data.UnpackInstanceId();
            _uiOrdinal = data.UnpackUInt32();
            _didRecipe = data.UnpackDataId();
        }
    }
}
