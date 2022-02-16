namespace AC2RE.Definitions;

public class CVertexArray {

    // CVertexArray
    public BBox bbox; // bbox
    public VertexFormatInfo vertexFormat; // vertexFormat
    public byte[] vertexData; // numVertices + vertices

    public CVertexArray(AC2Reader data) {
        vertexFormat = new(data.ReadUInt32());
        uint numVertices = data.ReadUInt32();
        vertexData = data.ReadBytes((int)(numVertices * vertexFormat.vertexSize));
        data.Align(4);
        bbox = new(data);
    }
}
