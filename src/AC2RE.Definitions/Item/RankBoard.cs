namespace AC2RE.Definitions;

public class RankBoard : gmCEntity {

    public override PackageType packageType => PackageType.RankBoard;

    public bool startAtBack; // m_bStartAtBack
    public int numVisibleEntries; // m_iNumVisibleEntries
    public WPString key; // m_sKey

    public RankBoard(AC2Reader data) : base(data) {
        startAtBack = data.ReadBoolean();
        numVisibleEntries = data.ReadInt32();
        data.ReadPkg<WPString>(v => key = v);
    }
}
