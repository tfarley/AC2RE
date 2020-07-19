using System.IO;

namespace AC2E.Def {

    public class GenericSEvt : IServerEvent {

        public ServerEventFunctionId funcId { get; private set; }

        public byte[] payload;

        public GenericSEvt(ServerEventFunctionId funcId, BinaryReader data, uint payloadLen) {
            this.funcId = funcId;
            payload = data.ReadBytes((int)payloadLen);
        }
    }
}
