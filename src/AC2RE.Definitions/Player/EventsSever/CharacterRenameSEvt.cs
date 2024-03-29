﻿namespace AC2RE.Definitions;

public class CharacterRenameSEvt : IServerEvent {

    public ServerEventFunctionId funcId => ServerEventFunctionId.Player__CharacterRename;

    // WM_Player::SendSEvt_CharacterRename
    public WPString name; // _name

    public CharacterRenameSEvt(AC2Reader data) {
        name = data.UnpackHeapObject<WPString>();
    }
}
