using System.Collections.Generic;

namespace AC2E.Def {

    public class EffectPulseMissedCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Effect__PulseMissed;

        // WM_Effect::PostCEvt_Effect_PulseMissed
        public List<uint> effectIds; // _effectEIDs

        public EffectPulseMissedCEvt() {

        }

        public EffectPulseMissedCEvt(AC2Reader data) {
            effectIds = data.UnpackPackage<AList>();
        }

        public void write(AC2Writer data) {
            data.Pack(AList.from(effectIds));
        }
    }
}
