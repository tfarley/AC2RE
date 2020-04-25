using AC2E.Def.Extensions;
using AC2E.Interp.Extensions;
using System.IO;

namespace AC2E.Protocol.Event.ClientEvents {

    public class ClientRemoveEffectCEvt : INetClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Effect__ClientRemoveEffect;

        public uint effectId;

        public void write(BinaryWriter data) {
            data.Pack(effectId);
        }
    }
}
