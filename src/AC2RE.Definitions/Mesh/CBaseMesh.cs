using System.Collections.Generic;

namespace AC2RE.Definitions;

public class CBaseMesh {

    // CBaseMesh
    public CVertexArray vertexArray; // m_pVertexArray
    public Sphere boundingSphere; // m_BoundingSphere
    public bool usesDynamicVertices; // m_bUsesDynamicVertices
    public MeshGeometry geometry; // m_pGeometry
    public List<DataId> materialInstanceDids; // m_pMaterials

    public CBaseMesh(AC2Reader data) {
        MeshType meshType = data.ReadEnum<MeshType>();
        materialInstanceDids = data.ReadList(data.ReadDataId);
        vertexArray = new(data);
        boundingSphere = new(data);
        geometry = new(meshType, data);
    }
}
