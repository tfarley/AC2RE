using System.IO;

namespace AC2E.Def {

    public class ClientAddEffectCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Effect__ClientAddEffect;

        // WM_Effect::PostCEvt_Effect_ClientAddEffect
        public EffectRecord _record;
        public uint _eid;

        public ClientAddEffectCEvt() {

        }

        public ClientAddEffectCEvt(BinaryReader data) {
            _record = data.UnpackPackage<EffectRecord>();
            _eid = data.UnpackUInt32();
        }

        public void write(BinaryWriter data) {
            data.Pack(_record);
            data.Pack(_eid);
        }
    }
}
