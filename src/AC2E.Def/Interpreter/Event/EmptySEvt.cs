namespace AC2E.Def {

    public class EmptySEvt : IServerEvent {

        public ServerEventFunctionId funcId { get; init; }

        public EmptySEvt(ServerEventFunctionId funcId) {
            this.funcId = funcId;
        }
    }
}
