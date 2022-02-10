namespace AC2RE.Definitions;

public class LoreFilter : IHeapObject {

    public PackageType packageType => PackageType.LoreFilter;

    public int minLore; // m_iMinLore
    public int maxLore; // m_iMaxLore

    public LoreFilter(AC2Reader data) {
        minLore = data.ReadInt32();
        maxLore = data.ReadInt32();
    }
}
