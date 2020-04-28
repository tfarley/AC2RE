using AC2E.Def.Extensions;
using AC2E.Interp.Extensions;
using AC2E.Interp.Packages;
using System.IO;

namespace AC2E.Interp.Event.ClientEvents {

    public class ClientAddEffectCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Effect__ClientAddEffect;

        public EffectRecordPkg _record;
        public uint _eid;

        public void write(BinaryWriter data) {
            data.Pack(_record);
            data.Pack(_eid);
        }
    }
}
