using AC2RE.Definitions;
using AC2RE.RenderLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AC2RE.Renderer;

internal class UberShaderManager : IDisposable {

    private static readonly string GLSL_VERSION = "#version 420 core";

    public static readonly uint POS_ATTRIB_ID = 0;
    private static readonly string NORMAL_DEFINE = "HAS_NORMAL";
    public static readonly uint NORMAL_ATTRIB_ID = 1;
    private static readonly string DIFFUSE_COLOR_DEFINE = "HAS_DIFFUSE_COLOR";
    public static readonly uint DIFFUSE_COLOR_ATTRIB_ID = 2;
    private static readonly string SPECULAR_COLOR_DEFINE = "HAS_SPECULAR_COLOR";
    public static readonly uint SPECULAR_COLOR_ATTRIB_ID = 3;
    private static readonly string TANGENT_DEFINE = "HAS_TANGENT";
    public static readonly uint TANGENT_ATTRIB_ID = 4;
    private static readonly string BITANGENT_DEFINE = "HAS_BITANGENT";
    public static readonly uint BITANGENT_ATTRIB_ID = 5;
    private static readonly string TEXCOORDS_DEFINE = "NUM_TEXCOORDS";
    public static readonly uint TEX_COORD_ATTRIB_ID_START = 6;
    private static readonly string MATRICES_DEFINE = "NUM_MATRICES";
    public static readonly uint MATRIX_INDICES_ATTRIB_ID_START = 8;
    public static readonly uint MATRIX_WEIGHTS_ATTRIB_ID_START = 12;

    private readonly Dictionary<uint, IShaderProgram> vertexFormatToShader = new();

    public void Dispose() {
        foreach (IShaderProgram shader in vertexFormatToShader.Values) {
            shader.Dispose();
        }
    }

    private byte[] buildUberShaderSource(byte[] shaderSource, List<string> defines) {
        MemoryStream vertexShaderSourceBuffer = new();
        using (StreamWriter data = new(vertexShaderSourceBuffer, Encoding.UTF8)) {
            data.WriteLine(GLSL_VERSION);
            foreach (string define in defines) {
                data.WriteLine($"#define {define}");
            }
            data.Write(Encoding.UTF8.GetString(shaderSource));
        }
        return vertexShaderSourceBuffer.ToArray();
    }

    public IShaderProgram getShader(IRenderer renderer, VertexFormatInfo vertexFormat) {
        if (!vertexFormatToShader.TryGetValue(vertexFormat.format, out IShaderProgram? shader)) {
            List<string> defines = new();
            if (vertexFormat.offsetNormal != 0) {
                defines.Add(NORMAL_DEFINE);
            }
            if (vertexFormat.offsetDiffuseColor != 0) {
                defines.Add(DIFFUSE_COLOR_DEFINE);
            }
            if (vertexFormat.offsetSpecularColor != 0) {
                defines.Add(SPECULAR_COLOR_DEFINE);
            }
            if (vertexFormat.offsetVectorS != 0) {
                defines.Add(TANGENT_DEFINE);
            }
            if (vertexFormat.offsetVectorT != 0) {
                defines.Add(BITANGENT_DEFINE);
            }
            if (vertexFormat.numTexCoordPairs > 0) {
                if (vertexFormat.numTexCoordPairs > 2) {
                    throw new ArgumentException("Number of texture coordinates exceeds shader support.");
                }
                defines.Add($"{TEXCOORDS_DEFINE} {vertexFormat.numTexCoordPairs}");
            }
            if (vertexFormat.numMatrices > 0) {
                if (vertexFormat.numMatrices > 4) {
                    throw new ArgumentException("Number of matrices exceeds shader support.");
                }
                defines.Add($"{MATRICES_DEFINE} {vertexFormat.numMatrices}");
            }

            byte[] vertShaderSource = buildUberShaderSource(Properties.Resources.uber_vert, defines);
            byte[] fragShaderSource = buildUberShaderSource(Properties.Resources.uber_frag, defines);

            using (IShader vertShader = renderer.loadVertexShader(vertShaderSource))
            using (IShader fragShader = renderer.loadFragmentShader(fragShaderSource)) {
                shader = renderer.createShaderProgram(vertShader, fragShader);
            }

            vertexFormatToShader[vertexFormat.format] = shader;
        }

        return shader;
    }
}
