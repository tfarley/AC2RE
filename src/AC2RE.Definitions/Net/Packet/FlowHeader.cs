namespace AC2RE.Definitions;

public class FlowHeader {

    // CFlowStruct
    public uint length; // cbDataRecvd
    public ushort interval; // interval

    public FlowHeader(AC2Reader data) {
        length = data.ReadUInt32();
        interval = data.ReadUInt16();
    }

    public void write(AC2Writer data) {
        data.Write(length);
        data.Write(interval);
    }
}
