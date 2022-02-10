using System.Collections.Generic;

namespace AC2RE.Definitions;

public class LevelMappingTable : IHeapObject {

    public PackageType packageType => PackageType.LevelMappingTable;

    public bool nonContiguous; // m_bNonContiguous
    public uint maxLevel; // m_maxLevel
    public List<ulong> map; // m_map
    public WPString name; // m_Name
    public uint minLevel; // m_minLevel

    public LevelMappingTable(AC2Reader data) {
        nonContiguous = data.ReadBoolean();
        maxLevel = data.ReadUInt32();
        data.ReadHO<LArray>(v => map = v);
        data.ReadHO<WPString>(v => name = v);
        minLevel = data.ReadUInt32();
    }
}
