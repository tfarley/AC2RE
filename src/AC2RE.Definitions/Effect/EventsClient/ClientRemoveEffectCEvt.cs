namespace AC2RE.Definitions;

public class ClientRemoveEffectCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Effect__ClientRemoveEffect;

    // WM_Effect::PostCEvt_Effect_ClientRemoveEffect
    public EffectId effectId; // _eid

    public ClientRemoveEffectCEvt() {

    }

    public ClientRemoveEffectCEvt(AC2Reader data) {
        effectId = data.UnpackEffectId();
    }

    public void write(AC2Writer data) {
        data.Pack(effectId);
    }
}
