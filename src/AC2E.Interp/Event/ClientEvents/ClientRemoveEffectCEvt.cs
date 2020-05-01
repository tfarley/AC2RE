using AC2E.Def.Extensions;
using AC2E.Interp.Extensions;
using System.IO;

namespace AC2E.Interp.Event.ClientEvents {

    public class ClientRemoveEffectCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Effect__ClientRemoveEffect;

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
