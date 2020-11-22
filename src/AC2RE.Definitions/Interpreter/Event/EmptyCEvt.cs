namespace AC2RE.Definitions {

    public class EmptyCEvt : IClientEvent {

        public ClientEventFunctionId funcId { get; init; }

        public EmptyCEvt(ClientEventFunctionId funcId) {
            this.funcId = funcId;
        }

        public void write(AC2Writer data) {

        }
    }
}
