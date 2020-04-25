using System.IO;

namespace AC2E.Protocol.Event.ServerEvents {

    public class GenericSEvt : INetServerEvent {

        public ServerEventFunctionId funcId { get; private set; }

        public byte[] payload;

        public GenericSEvt(ServerEventFunctionId funcId, BinaryReader data, uint payloadLen) {
            this.funcId = funcId;
            payload = data.ReadBytes((int)payloadLen);
        }
    }
}
