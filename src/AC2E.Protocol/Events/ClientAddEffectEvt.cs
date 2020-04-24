using AC2E.Def.Extensions;
using AC2E.Interp;
using AC2E.Interp.Extensions;
using System.IO;

namespace AC2E.Protocol.Messages {

    public class ClientAddEffectEvt : INetEvent {

        public FunctionId funcId => (uint)EventFunctionId.ClientAddEffect;

        public EffectRecordPkg effectRecord;
        public uint effectId;

        public void write(BinaryWriter data) {
            data.Pack(effectRecord);
            data.Pack(effectId);
        }
    }
}
