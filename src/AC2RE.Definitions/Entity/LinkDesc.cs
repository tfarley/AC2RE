namespace AC2RE.Definitions;

public class LinkDesc {

    // LinkDesc
    public InstanceId id; // m_id
    public uint type; // m_type

    public LinkDesc(AC2Reader data) {
        // TODO: Untested parsing
        throw new System.Exception();
        id = data.ReadInstanceId();
        type = data.ReadUInt32();
    }
}
