namespace AC2E.Def {

    public class ClientAddEffectCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Effect__ClientAddEffect;

        // WM_Effect::PostCEvt_Effect_ClientAddEffect
        public EffectRecord effectRecord; // _record
        public uint effectId; // _eid

        public ClientAddEffectCEvt() {

        }

        public ClientAddEffectCEvt(AC2Reader data) {
            effectRecord = data.UnpackPackage<EffectRecord>();
            effectId = data.UnpackUInt32();
        }

        public void write(AC2Writer data) {
            data.Pack(effectRecord);
            data.Pack(effectId);
        }
    }
}
