﻿namespace AC2RE.Definitions;

public class CreateFellowshipSEvt : IServerEvent {

    public ServerEventFunctionId funcId => ServerEventFunctionId.Fellowship__CreateFellowship;

    // WM_Fellowship::SendSEvt_CreateFellowship
    public LootingMethod lootingMethod; // _lootingMethod
    public bool social; // _social
    public WPString name; // _name

    public CreateFellowshipSEvt(AC2Reader data) {
        lootingMethod = data.UnpackEnum<LootingMethod>();
        social = data.UnpackBoolean();
        name = data.UnpackHeapObject<WPString>();
    }
}
