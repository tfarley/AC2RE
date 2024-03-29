﻿namespace AC2RE.Definitions;

public class ToggleSpecialAttackCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Combat__ToggleSpecialAttack;

    // WM_Combat::PostCEvt_ToggleSpecialAttack
    public bool toggled; // _toggled
    public SkillId skill; // _maneuver

    public ToggleSpecialAttackCEvt() {

    }

    public ToggleSpecialAttackCEvt(AC2Reader data) {
        toggled = data.UnpackBoolean();
        skill = data.UnpackEnum<SkillId>();
    }

    public void write(AC2Writer data) {
        data.Pack(toggled);
        data.PackEnum(skill);
    }
}
