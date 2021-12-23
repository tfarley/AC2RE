namespace AC2RE.Definitions;

public class CAExcavationPoint : Container {

    public override PackageType packageType => PackageType.CAExcavationPoint;

    public StringInfo breakText; // m_breaktext
    public StringInfo noHandsText; // m_nohandstext
    public StringInfo lootText; // m_loottext

    public CAExcavationPoint(AC2Reader data) : base(data) {
        data.ReadPkg<StringInfo>(v => breakText = v);
        data.ReadPkg<StringInfo>(v => noHandsText = v);
        data.ReadPkg<StringInfo>(v => lootText = v);
    }
}
