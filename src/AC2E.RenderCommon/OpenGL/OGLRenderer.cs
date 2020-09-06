using System;
using System.Collections.Generic;
using Veldrid.OpenGLBinding;
using static AC2E.RenderCommon.OpenGL.OGLUtil;
using static Veldrid.OpenGLBinding.OpenGLNative;

namespace AC2E.RenderCommon.OpenGL {

    internal abstract class OGLRenderer : IRenderer {

        public static readonly uint VIEW_DATA_UBO_BINDING = 0;
        public static readonly uint LIGHT_DATA_UBO_BINDING = 1;

        private uint viewDataUboId;
        private uint lightDataUboId;
        protected IShaderProgram defaultShader;

        protected void init() {
            glClearColor(1.0f, 0.0f, 0.0f, 1.0f);
            checkError();

            glGenBuffers(1, out viewDataUboId);
            checkError();

            glGenBuffers(1, out lightDataUboId);
            checkError();

            using (IShader defaultVertShader = loadVertexShader(Properties.Resources.default_vert))
            using (IShader defaultFragShader = loadFragmentShader(Properties.Resources.default_frag)) {
                defaultShader = createShaderProgram(defaultVertShader, defaultFragShader);
            }
        }

        public virtual void Dispose() {
            defaultShader.Dispose();
        }

        public void resize(uint width, uint height) {
            glViewportIndexed(0, 0, 0, width, height);
        }

        public void setModelToClipTransform(float[] modelToClipMatrix) {
            if (modelToClipMatrix.Length != 16) {
                throw new ArgumentException($"4x4 matrix must have exactly 16 elements but it had {modelToClipMatrix.Length}", "modelToClipMatrix");
            }

            byte[] matrixData = new byte[modelToClipMatrix.Length * sizeof(float)];
            Buffer.BlockCopy(modelToClipMatrix, 0, matrixData, 0, matrixData.Length);

            setUboData(VIEW_DATA_UBO_BINDING, viewDataUboId, matrixData);
        }

        public void setModelToClipTransform(float[,] modelToClipMatrix) {
            if (modelToClipMatrix.Length != 16) {
                throw new ArgumentException($"4x4 matrix must have exactly 16 elements but it had {modelToClipMatrix.Length}", "modelToClipMatrix");
            }

            byte[] matrixData = new byte[modelToClipMatrix.Length * sizeof(float)];
            Buffer.BlockCopy(modelToClipMatrix, 0, matrixData, 0, matrixData.Length);

            setUboData(VIEW_DATA_UBO_BINDING, viewDataUboId, matrixData);
        }

        public void setLightData(byte[] lightData) {
            setUboData(LIGHT_DATA_UBO_BINDING, lightDataUboId, lightData);
        }

        private void setUboData(uint uboBinding, uint uboId, byte[] data) {
            glBindBufferRange(BufferRangeTarget.UniformBuffer, uboBinding, uboId, (IntPtr)0, (UIntPtr)data.Length);
            checkError();

            unsafe {
                fixed (byte* dataPtr = data) {
                    glBufferData(BufferTarget.UniformBuffer, (UIntPtr)data.Length, dataPtr, BufferUsageHint.StreamDraw);
                    checkError();
                }
            }
        }

        public IShader loadVertexShader(byte[] shaderSource) {
            return new OGLShader(shaderSource, ShaderType.VertexShader);
        }

        public IShader loadFragmentShader(byte[] shaderSource) {
            return new OGLShader(shaderSource, ShaderType.FragmentShader);
        }

        public IShaderProgram createShaderProgram(params IShader[] shaders) {
            return new OGLShaderProgram(shaders);
        }

        public IMesh loadMesh(byte[] vertexData, List<VertexAttribute> vertexAttributes, uint vertexSize, byte[] elementData, Type elementType) {
            return new OGLMesh(vertexData, vertexAttributes, vertexSize, elementData, elementType);
        }

        public void draw(IMesh mesh, IShaderProgram shader) {
            if (shader == null) {
                shader = defaultShader;
            }

            OGLShaderProgram oglShader = (OGLShaderProgram)shader;
            oglShader.use();

            OGLMesh oglMesh = (OGLMesh)mesh;
            oglMesh.draw();
        }

        public void clear() {
            glClear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            checkError();
        }

        public abstract void swap();
    }
}
