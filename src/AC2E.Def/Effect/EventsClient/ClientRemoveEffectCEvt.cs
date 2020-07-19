using System.IO;

namespace AC2E.Def {

    public class ClientRemoveEffectCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Effect__ClientRemoveEffect;

        // WM_Effect::PostCEvt_Effect_ClientRemoveEffect
        public uint _eid;

        public ClientRemoveEffectCEvt() {

        }

        public ClientRemoveEffectCEvt(BinaryReader data) {
            _eid = data.UnpackUInt32();
        }

        public void write(BinaryWriter data) {
            data.Pack(_eid);
        }
    }
}
