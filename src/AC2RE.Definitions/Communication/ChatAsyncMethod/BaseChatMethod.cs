namespace AC2RE.Definitions;

public abstract class BaseChatMethod : IChatAsyncMethod {

    // Request + Response
    public uint contextId; // m_contextID
    public uint requestId; // dwRequestID

    public BaseChatMethod() {

    }

    public BaseChatMethod(AC2Reader data) {
        contextId = data.ReadUInt32();
        requestId = data.ReadUInt32();
    }

    public virtual void write(AC2Writer data) {
        data.Write(contextId);
        data.Write(requestId);
    }
}
