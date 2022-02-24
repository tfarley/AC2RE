namespace AC2RE.Definitions;

public class CliDatErrorMsg : INetMessage {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.OUT_OF_WORLD;
    public NetQueue queueId => NetQueue.DATABASE;
    public MessageOpcode opcode => MessageOpcode.CLIDAT_ERROR_EVENT;
    public OrderingType orderingType => OrderingType.UNORDERED;

    // CCliDatErrorEvent::CDataFormat
    public QualifiedDataId qdid; // qdid
    public uint error; // dwError

    public CliDatErrorMsg() {

    }

    public CliDatErrorMsg(AC2Reader data) {
        qdid = data.ReadQualifiedDataId();
        error = data.ReadUInt32();
    }

    public void write(AC2Writer data) {
        data.Write(qdid);
        data.Write(error);
    }
}
