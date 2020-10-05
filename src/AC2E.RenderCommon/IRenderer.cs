using AC2E.RenderCommon.OpenGL;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace AC2E.RenderCommon {

    public interface IRenderer : IDisposable {

        static IRenderer createWinOGL(IntPtr hwnd) {
            return new WinOGLRenderer(hwnd);
        }

        void resize(uint width, uint height);

        void setClearColor(float r, float g, float b);

        // NOTE: Matrix4x4 transform multiplication order is left-to-right; OpenGl mat order is right-to-left
        void setTransforms(Matrix4x4 modelToClipMatrix, Matrix4x4 modelToCameraMatrix);

        void setAmbientLight(float r, float g, float b);
        void setDirLight(DirLight dirLight);
        void setPointLightData(byte[] pointLightData);

        IShader loadVertexShader(byte[] shaderSource);
        IShader loadFragmentShader(byte[] shaderSource);
        IShaderProgram createShaderProgram(params IShader[] shaders);

        IMesh loadMesh(byte[] vertexData, List<VertexAttribute> vertexAttributes, uint vertexSize, byte[] indexData, Type indexType);

        ITexture loadTexture(byte[] textureData, uint width, uint height, TextureFormat format);

        void draw(IMesh mesh, IShaderProgram? shader = null, List<ITexture>? textures = null);

        void clear();

        void swap();
    }
}
