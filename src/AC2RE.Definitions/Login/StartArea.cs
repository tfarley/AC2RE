using System.Collections.Generic;

namespace AC2RE.Definitions;

public class StartArea : IHeapObject {

    public PackageType packageType => PackageType.StartArea;

    public uint startingLocType; // m_startingLocType
    public List<Position> positions; // m_posList
    public WPString description; // m_desc

    public StartArea(AC2Reader data) {
        startingLocType = data.ReadUInt32();
        data.ReadHO<RList>(v => positions = v.to<Position>());
        data.ReadHO<WPString>(v => description = v);
    }
}
