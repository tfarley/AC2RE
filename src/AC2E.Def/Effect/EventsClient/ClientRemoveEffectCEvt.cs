namespace AC2E.Def {

    public class ClientRemoveEffectCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Effect__ClientRemoveEffect;

        // WM_Effect::PostCEvt_Effect_ClientRemoveEffect
        public uint _eid;

        public ClientRemoveEffectCEvt() {

        }

        public ClientRemoveEffectCEvt(AC2Reader data) {
            _eid = data.UnpackUInt32();
        }

        public void write(AC2Writer data) {
            data.Pack(_eid);
        }
    }
}
