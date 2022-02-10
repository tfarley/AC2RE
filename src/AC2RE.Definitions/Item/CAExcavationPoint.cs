namespace AC2RE.Definitions;

public class CAExcavationPoint : Container {

    public override PackageType packageType => PackageType.CAExcavationPoint;

    public StringInfo breakText; // m_breaktext
    public StringInfo noHandsText; // m_nohandstext
    public StringInfo lootText; // m_loottext

    public CAExcavationPoint(AC2Reader data) : base(data) {
        data.ReadHO<StringInfo>(v => breakText = v);
        data.ReadHO<StringInfo>(v => noHandsText = v);
        data.ReadHO<StringInfo>(v => lootText = v);
    }
}
