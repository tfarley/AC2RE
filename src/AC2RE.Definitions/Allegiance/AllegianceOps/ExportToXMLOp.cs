namespace AC2RE.Definitions;

public class ExportToXMLOp : IHeapObject {

    public PackageType packageType => PackageType.ExportToXMLOp;

    public uint nodesSeen; // m_cNodesSeen
    public uint indentLevel; // m_indentLevel
    public WPString fileName; // m_file

    public ExportToXMLOp(AC2Reader data) {
        nodesSeen = data.ReadUInt32();
        indentLevel = data.ReadUInt32();
        data.ReadHO<WPString>(v => fileName = v);
    }
}
