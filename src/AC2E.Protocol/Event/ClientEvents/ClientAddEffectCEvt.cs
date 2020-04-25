using AC2E.Def.Extensions;
using AC2E.Interp;
using AC2E.Interp.Extensions;
using System.IO;

namespace AC2E.Protocol.Event.ClientEvents {

    public class ClientAddEffectCEvt : INetClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Effect__ClientAddEffect;

        public EffectRecordPkg effectRecord;
        public uint effectId;

        public void write(BinaryWriter data) {
            data.Pack(effectRecord);
            data.Pack(effectId);
        }
    }
}
