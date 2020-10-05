using AC2E.Def;
using AC2E.RenderCommon;
using System.Collections.Generic;

namespace AC2E.Render {

    public class RenderMesh {

        public readonly IMesh mesh;
        public readonly VertexFormatInfo vertexFormat;
        public readonly List<ITexture> textures;

        public RenderMesh(IMesh mesh, VertexFormatInfo vertexFormat, List<ITexture> textures) {
            this.mesh = mesh;
            this.vertexFormat = vertexFormat;
            this.textures = textures;
        }
    }
}
