namespace AC2RE.Definitions;

public class DurabilityFilter : IHeapObject {

    public PackageType packageType => PackageType.DurabilityFilter;

    public float minDurability; // m_fMinDurability
    public float maxDurability; // m_fMaxDurability

    public DurabilityFilter(AC2Reader data) {
        minDurability = data.ReadSingle();
        maxDurability = data.ReadSingle();
    }
}
