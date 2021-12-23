namespace AC2RE.Definitions;

public class RecipeCostData : IPackage {

    public PackageType packageType => PackageType.RecipeCostData;

    public int baseCost; // m_baseCost
    public int uncommonCost; // m_uncommonCost
    public int elderCost; // m_elderCost

    public RecipeCostData(AC2Reader data) {
        baseCost = data.ReadInt32();
        uncommonCost = data.ReadInt32();
        elderCost = data.ReadInt32();
    }
}
