using AC2RE.RenderLib.OpenGL;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace AC2RE.RenderLib;

public interface IRenderer : IDisposable {

    public static IRenderer createWinOGL(IntPtr hwnd) {
        return new WinOGLRenderer(hwnd);
    }

    public void resize(uint width, uint height);

    public void setClearColor(float r, float g, float b);

    // NOTE: Matrix4x4 transform multiplication order is left-to-right; OpenGl mat order is right-to-left
    public void setTransforms(Matrix4x4 modelToClipMatrix, Matrix4x4 modelToCameraMatrix);

    public void setAmbientLight(float r, float g, float b);
    public void setDirLight(DirLight dirLight);
    public void setPointLightData(byte[] pointLightData);

    public IShader loadVertexShader(byte[] shaderSource);
    public IShader loadFragmentShader(byte[] shaderSource);
    public IShaderProgram createShaderProgram(params IShader[] shaders);

    public IMesh loadMesh(byte[] vertexData, List<VertexAttribute> vertexAttributes, uint vertexSize, byte[] indexData, Type indexType);

    public ITexture loadTexture(byte[] textureData, uint width, uint height, TextureFormat format);

    public void draw(IMesh mesh, IShaderProgram? shader = null, List<ITexture>? textures = null);

    public void clear();

    public void swap();
}
