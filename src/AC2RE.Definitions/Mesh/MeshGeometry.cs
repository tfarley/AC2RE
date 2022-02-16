using System.Collections.Generic;

namespace AC2RE.Definitions;

public class MeshGeometry {

    // MeshGeometry
    public MeshType meshType; // mesh_type
    public List<CMeshTriList> degrades; // m_pDegradeList

    public MeshGeometry(MeshType meshType, AC2Reader data) {
        this.meshType = meshType;
        degrades = data.ReadList(() => new CMeshTriList(data));
    }
}
