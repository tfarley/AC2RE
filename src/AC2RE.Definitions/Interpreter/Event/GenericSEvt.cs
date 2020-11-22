namespace AC2RE.Definitions {

    public class GenericSEvt : IServerEvent {

        public ServerEventFunctionId funcId { get; init; }

        public byte[] payload;

        public GenericSEvt(ServerEventFunctionId funcId, AC2Reader data, uint payloadLen) {
            this.funcId = funcId;
            payload = data.ReadBytes((int)payloadLen);
        }
    }
}
