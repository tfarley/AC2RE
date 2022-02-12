namespace AC2RE.Definitions;

public class InterpSEventMsg : INetMessage {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
    public NetQueue queueId => NetQueue.WEENIE;
    public MessageOpcode opcode => MessageOpcode.Interp__InterpSEvent;

    // ECM_Interp::SendEvt_InterpSEvent
    public IServerEvent netEvent;

    public InterpSEventMsg(AC2Reader data) {
        ServerEventFunctionId funcId = data.ReadEnum<ServerEventFunctionId>();
        uint length = data.ReadUInt32();
        netEvent = IServerEvent.read(funcId, data);
    }
}
