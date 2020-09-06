using AC2E.RenderCommon.OpenGL;
using System;
using System.Collections.Generic;

namespace AC2E.RenderCommon {

    public interface IRenderer : IDisposable {

        static IRenderer createWinOGL(IntPtr hwnd) {
            return new WinOGLRenderer(hwnd);
        }

        void resize(uint width, uint height);

        void setModelToClipTransform(float[] modelToClipMatrix);
        void setModelToClipTransform(float[,] modelToClipMatrix);
        void setLightData(byte[] lightData);

        IShader loadVertexShader(byte[] shaderSource);
        IShader loadFragmentShader(byte[] shaderSource);
        IShaderProgram createShaderProgram(params IShader[] shaders);

        IMesh loadMesh(byte[] vertexData, List<VertexAttribute> vertexAttributes, uint vertexSize, byte[] elementData, Type elementType);

        void draw(IMesh mesh, IShaderProgram shader = null);

        void clear();

        void swap();
    }
}
