namespace AC2RE.Definitions;

public class ConsignmentDesc : IPackage {

    public PackageType packageType => PackageType.ConsignmentDesc;

    public uint saleId; // m_saleID
    public bool expired; // m_bExpired
    public int sold; // m_iSold

    public ConsignmentDesc(AC2Reader data) {
        saleId = data.ReadUInt32();
        expired = data.ReadBoolean();
        sold = data.ReadInt32();
    }
}
