using AC2E.Def.Extensions;
using AC2E.Interp;
using AC2E.Interp.Extensions;
using System.IO;

namespace AC2E.Protocol.Messages {

    public class ClientRemoveEffectEvt : INetEvent {

        public FunctionId funcId => (uint)EventFunctionId.ClientRemoveEffect;

        public uint effectId;

        public void write(BinaryWriter data) {
            data.Pack(effectId);
        }
    }
}
