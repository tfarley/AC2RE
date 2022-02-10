using System.Collections.Generic;

namespace AC2RE.Definitions;

public class LevelTable : IHeapObject {

    public PackageType packageType => PackageType.LevelTable;

    public List<LevelData> map; // mMap
    public uint maxLevel; // mMaxLevel

    public LevelTable(AC2Reader data) {
        data.ReadHO<RArray>(v => map = v.to<LevelData>());
        maxLevel = data.ReadUInt32();
    }
}
