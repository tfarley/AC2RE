using System.Collections.Generic;
using System.Numerics;

namespace AC2E.Render {

    public class RenderObject {

        public readonly List<RenderMesh> meshes;
        public Vector3 pos;
        public Quaternion rot;

        public RenderObject(List<RenderMesh> meshes) {
            this.meshes = meshes;
        }
    }
}
