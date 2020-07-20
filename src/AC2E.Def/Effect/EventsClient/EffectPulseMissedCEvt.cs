namespace AC2E.Def {

    public class EffectPulseMissedCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Effect__PulseMissed;

        // WM_Effect::PostCEvt_Effect_PulseMissed
        public AList _effectEIDs;

        public EffectPulseMissedCEvt() {

        }

        public EffectPulseMissedCEvt(AC2Reader data) {
            _effectEIDs = data.UnpackPackage<AList>();
        }

        public void write(AC2Writer data) {
            data.Pack(_effectEIDs);
        }
    }
}
