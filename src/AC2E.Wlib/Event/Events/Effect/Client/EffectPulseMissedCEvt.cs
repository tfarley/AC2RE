using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class EffectPulseMissedCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Effect__PulseMissed;

        // WM_Effect::PostCEvt_Effect_PulseMissed
        public AList _effectEIDs;

        public EffectPulseMissedCEvt() {

        }

        public EffectPulseMissedCEvt(BinaryReader data) {
            _effectEIDs = data.UnpackPackage<AList>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_effectEIDs);
        }
    }
}
