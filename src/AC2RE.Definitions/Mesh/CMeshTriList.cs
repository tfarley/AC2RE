using System.Collections.Generic;

namespace AC2RE.Definitions;

public class CMeshTriList {

    // CMeshTriList
    public DegradeType degradeType; // degrade_type
    public uint detailLevels; // m_DetailLevels
    public List<CMeshFragment> materials; // num_materials + material_lists

    public CMeshTriList(AC2Reader data) {
        degradeType = data.ReadEnum<DegradeType>();
        detailLevels = data.ReadUInt32();
        materials = data.ReadList(() => new CMeshFragment(data));
    }
}
