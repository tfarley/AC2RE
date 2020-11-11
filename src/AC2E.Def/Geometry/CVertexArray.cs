namespace AC2E.Def {

    public class CVertexArray {

        public BBox bbox; // bbox
        public VertexFormatInfo vertexFormat; // vertexFormat
        public byte[] vertexData; // numVertices + vertices

        public CVertexArray(AC2Reader data) {
            vertexFormat = new(data.ReadUInt32());
            uint numVertices = data.ReadUInt32();
            vertexData = data.ReadBytes((int)(numVertices * vertexFormat.vertexSize));
            data.Align(4);
            bbox = new(data);
            // TODO: Check to see if there is more to parse here
        }
    }
}
