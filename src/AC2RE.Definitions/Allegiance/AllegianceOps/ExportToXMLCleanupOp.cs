namespace AC2RE.Definitions;

public class ExportToXMLCleanupOp : IPackage {

    public PackageType packageType => PackageType.ExportToXMLCleanupOp;

    public ExportToXMLOp exportOp; // m_exportOp
    public WPString fileName; // m_file

    public ExportToXMLCleanupOp(AC2Reader data) {
        data.ReadPkg<ExportToXMLOp>(v => exportOp = v);
        data.ReadPkg<WPString>(v => fileName = v);
    }
}
