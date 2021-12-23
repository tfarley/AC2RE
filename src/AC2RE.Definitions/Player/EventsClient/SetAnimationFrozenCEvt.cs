namespace AC2RE.Definitions;

public class SetAnimationFrozenCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Player__SetAnimationFrozen;

    // WM_Player::PostCEvt_SetAnimationFrozen
    public bool frozen; // _bFrozen

    public SetAnimationFrozenCEvt() {

    }

    public SetAnimationFrozenCEvt(AC2Reader data) {
        frozen = data.UnpackBoolean();
    }

    public void write(AC2Writer data) {
        data.Pack(frozen);
    }
}
