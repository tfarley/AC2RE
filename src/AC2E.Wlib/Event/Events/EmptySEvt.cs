namespace AC2E.WLib {

    public class EmptySEvt : IServerEvent {

        public ServerEventFunctionId funcId { get; private set; }

        public EmptySEvt(ServerEventFunctionId funcId) {
            this.funcId = funcId;
        }
    }
}
