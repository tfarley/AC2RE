using System.Collections.Generic;

namespace AC2E.Def {

    public class CBaseMesh {

        public CVertexArray vertexArray; // m_pVertexArray
        public Sphere boundingSphere; // m_BoundingSphere
        public bool usesDynamicVertices; // m_bUsesDynamicVertices
        public MeshGeometry geometry; // m_pGeometry
        public List<DataId> materialInstanceDids; // m_pMaterials

        public CBaseMesh(AC2Reader data) {
            uint geometryMeshType = data.ReadUInt32();
            materialInstanceDids = data.ReadList(data.ReadDataId);
            vertexArray = new CVertexArray(data);
            boundingSphere = new Sphere(data);
            geometry = new MeshGeometry(geometryMeshType, data);
            // TODO: Check to see if there is more to parse here
        }
    }
}
