using System.IO;

namespace AC2RE.Definitions;

public interface IInterpCEventMsg {

    public IClientEvent netEvent { get; }
}

public class InterpCEventCellMsg : INetMessage, IInterpCEventMsg {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
    public NetQueue queueId => NetQueue.EVENT;
    public MessageOpcode opcode => MessageOpcode.Interp__InterpCEvent_Cell;
    public OrderingType orderingType => OrderingType.UNORDERED;

    // ECM_Interp::RecvEvt_InterpCEvent_Cell
    public InstanceIdWithStamp senderIdWithStamp; // sender
    public IClientEvent netEvent { get; set; }

    public InterpCEventCellMsg() {

    }

    public InterpCEventCellMsg(AC2Reader data) {
        senderIdWithStamp = data.ReadInstanceIdWithStamp();
        ClientEventFunctionId funcId = data.ReadEnum<ClientEventFunctionId>();
        uint length = data.ReadUInt32();
        netEvent = IClientEvent.read(funcId, data);
    }

    public void write(AC2Writer data) {
        data.Write(senderIdWithStamp);
        data.WriteEnum(netEvent.funcId);
        // Placeholder for length
        data.Write((uint)0);
        long contentStart = data.BaseStream.Position;
        netEvent.write(data);
        long contentEnd = data.BaseStream.Position;
        long contentLength = contentEnd - contentStart;
        data.BaseStream.Seek(contentStart - sizeof(uint), SeekOrigin.Begin);
        data.Write((uint)contentLength);
        data.BaseStream.Seek(contentEnd, SeekOrigin.Begin);
    }
}

public class InterpCEventPrivateMsg : INetMessage, IInterpCEventMsg {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
    public NetQueue queueId => NetQueue.EVENT;
    public MessageOpcode opcode => MessageOpcode.Interp__InterpCEvent_Private;
    public OrderingType orderingType => OrderingType.PRIVATE_ORDERED;

    // ECM_Interp::RecvEvt_InterpCEvent_Private
    public IClientEvent netEvent { get; set; }

    public InterpCEventPrivateMsg() {

    }

    public InterpCEventPrivateMsg(AC2Reader data) {
        ClientEventFunctionId funcId = data.ReadEnum<ClientEventFunctionId>();
        uint length = data.ReadUInt32();
        netEvent = IClientEvent.read(funcId, data);
    }

    public void write(AC2Writer data) {
        data.WriteEnum(netEvent.funcId);
        // Placeholder for length
        data.Write((uint)0);
        long contentStart = data.BaseStream.Position;
        netEvent.write(data);
        long contentEnd = data.BaseStream.Position;
        long contentLength = contentEnd - contentStart;
        data.BaseStream.Seek(contentStart - sizeof(uint), SeekOrigin.Begin);
        data.Write((uint)contentLength);
        data.BaseStream.Seek(contentEnd, SeekOrigin.Begin);
    }
}

public class InterpCEventVisualMsg : INetMessage, IInterpCEventMsg {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
    public NetQueue queueId => NetQueue.EVENT;
    public MessageOpcode opcode => MessageOpcode.Interp__InterpCEvent_Visual;
    public OrderingType orderingType => OrderingType.VISUAL_ORDERED;

    // ECM_Interp::RecvEvt_InterpCEvent_Visual
    public InstanceIdWithStamp senderIdWithStamp; // sender
    public IClientEvent netEvent { get; set; }

    public InterpCEventVisualMsg() {

    }

    public InterpCEventVisualMsg(AC2Reader data) {
        senderIdWithStamp = data.ReadInstanceIdWithStamp();
        ClientEventFunctionId funcId = data.ReadEnum<ClientEventFunctionId>();
        uint length = data.ReadUInt32();
        netEvent = IClientEvent.read(funcId, data);
    }

    public void write(AC2Writer data) {
        data.Write(senderIdWithStamp);
        data.WriteEnum(netEvent.funcId);
        // Placeholder for length
        data.Write((uint)0);
        long contentStart = data.BaseStream.Position;
        netEvent.write(data);
        long contentEnd = data.BaseStream.Position;
        long contentLength = contentEnd - contentStart;
        data.BaseStream.Seek(contentStart - sizeof(uint), SeekOrigin.Begin);
        data.Write((uint)contentLength);
        data.BaseStream.Seek(contentEnd, SeekOrigin.Begin);
    }
}
