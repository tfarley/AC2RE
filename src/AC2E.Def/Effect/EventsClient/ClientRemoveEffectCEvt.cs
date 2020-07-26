namespace AC2E.Def {

    public class ClientRemoveEffectCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Effect__ClientRemoveEffect;

        // WM_Effect::PostCEvt_Effect_ClientRemoveEffect
        public uint effectId; // _eid

        public ClientRemoveEffectCEvt() {

        }

        public ClientRemoveEffectCEvt(AC2Reader data) {
            effectId = data.UnpackUInt32();
        }

        public void write(AC2Writer data) {
            data.Pack(effectId);
        }
    }
}
