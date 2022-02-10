namespace AC2RE.Definitions;

public class ExportToXMLCleanupOp : IHeapObject {

    public PackageType packageType => PackageType.ExportToXMLCleanupOp;

    public ExportToXMLOp exportOp; // m_exportOp
    public WPString fileName; // m_file

    public ExportToXMLCleanupOp(AC2Reader data) {
        data.ReadHO<ExportToXMLOp>(v => exportOp = v);
        data.ReadHO<WPString>(v => fileName = v);
    }
}
