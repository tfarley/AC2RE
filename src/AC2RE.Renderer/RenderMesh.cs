using AC2RE.Definitions;
using AC2RE.RenderLib;
using System.Collections.Generic;

namespace AC2RE.Renderer {

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
