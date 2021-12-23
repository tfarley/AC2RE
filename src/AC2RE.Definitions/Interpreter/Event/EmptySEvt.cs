namespace AC2RE.Definitions;

public class EmptySEvt : IServerEvent {

    public ServerEventFunctionId funcId { get; init; }

    public EmptySEvt(ServerEventFunctionId funcId) {
        this.funcId = funcId;
    }
}
