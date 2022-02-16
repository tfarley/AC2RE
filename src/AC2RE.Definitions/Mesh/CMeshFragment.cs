using System.Collections.Generic;

namespace AC2RE.Definitions;

public class CMeshFragment {

    // CMeshFragment
    public uint materialIndex; // m_nMaterialIndex
    public List<ushort> indices; // m_pIndexBuffer

    public CMeshFragment(AC2Reader data) {
        materialIndex = data.ReadUInt32();
        indices = data.ReadList(data.ReadUInt16);
        data.Align(4);
    }
}
