﻿namespace AC2E.Def {

    public class CreateFellowshipSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Fellowship__CreateFellowship;

        // WM_Fellowship::SendSEvt_CreateFellowship
        public uint lootingMethod; // _lootingMethod
        public bool social; // _social
        public WPString name; // _name

        public CreateFellowshipSEvt(AC2Reader data) {
            lootingMethod = data.UnpackUInt32();
            social = data.UnpackBoolean();
            name = data.UnpackPackage<WPString>();
        }
    }
}
