using System.Collections.Generic;

namespace AC2RE.Definitions;

public class RecipeNameColoringTable : IHeapObject {

    public PackageType packageType => PackageType.RecipeNameColoringTable;

    public List<uint> map; // m_map
    public int maxDiff; // m_maxDiff
    public int minDiff; // m_minDiff

    public RecipeNameColoringTable(AC2Reader data) {
        data.ReadHO<AArray>(v => map = v);
        maxDiff = data.ReadInt32();
        minDiff = data.ReadInt32();
    }
}
