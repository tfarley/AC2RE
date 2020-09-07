using AC2E.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using Veldrid.OpenGLBinding;
using static AC2E.RenderCommon.OpenGL.OGLUtil;
using static Veldrid.OpenGLBinding.OpenGLNative;

namespace AC2E.RenderCommon.OpenGL {

    internal abstract class OGLRenderer : IRenderer {

        public static readonly uint VIEW_DATA_UBO_BINDING = 0;
        public static readonly uint AMBIENT_LIGHT_DATA_UBO_BINDING = 1;
        public static readonly uint DIR_LIGHT_DATA_UBO_BINDING = 2;
        public static readonly uint POINT_LIGHT_DATA_UBO_BINDING = 3;

        private uint viewDataUboId;
        private uint ambientLightDataUboId;
        private uint dirLightDataUboId;
        private uint pointLightDataUboId;
        protected IShaderProgram defaultShader;

        protected void init() {
            glGenBuffers(1, out viewDataUboId);
            checkError();

            glGenBuffers(1, out ambientLightDataUboId);
            checkError();

            glGenBuffers(1, out dirLightDataUboId);
            checkError();

            glGenBuffers(1, out pointLightDataUboId);
            checkError();

            using (IShader vertShader = loadVertexShader(Properties.Resources.default_vert))
            using (IShader fragShader = loadFragmentShader(Properties.Resources.default_frag)) {
                defaultShader = createShaderProgram(vertShader, fragShader);
            }

            glEnable(EnableCap.DepthTest);
            checkError();

            glDepthFunc(DepthFunction.Lequal);
            checkError();

            glEnable(EnableCap.CullFace);
            checkError();

            glCullFace(CullFaceMode.Back);
            checkError();
        }

        public virtual void Dispose() {
            defaultShader.Dispose();
        }

        public void resize(uint width, uint height) {
            glViewportIndexed(0, 0, 0, width, height);
        }

        public void setClearColor(float r, float g, float b) {
            glClearColor(r, g, b, 1.0f);
            checkError();
        }

        public void setTransforms(Matrix4x4 modelToClipMatrix, Matrix4x4 modelToCameraMatrix) {
            int matrixSize = 16 * sizeof(float);
            byte[] matrixData = new byte[matrixSize * 2];
            Buffer.BlockCopy(modelToClipMatrix.ToFloats(), 0, matrixData, 0, matrixSize);
            Buffer.BlockCopy(modelToCameraMatrix.ToFloats(), 0, matrixData, matrixSize, matrixSize);

            setUboData(VIEW_DATA_UBO_BINDING, viewDataUboId, matrixData);
        }

        public void setAmbientLight(float r, float g, float b) {
            MemoryStream buffer = new MemoryStream();
            using (BinaryWriter data = new BinaryWriter(buffer)) {
                data.Write(r);
                data.Write(g);
                data.Write(b);
                data.Write(0.0f); // Pad
            }

            setUboData(AMBIENT_LIGHT_DATA_UBO_BINDING, ambientLightDataUboId, buffer.ToArray());
        }

        public void setDirLight(DirLight dirLight) {
            Vector3 dirNormalized = Vector3.Normalize(dirLight.dir);
            MemoryStream buffer = new MemoryStream();
            using (BinaryWriter data = new BinaryWriter(buffer)) {
                data.Write(dirNormalized.X);
                data.Write(dirNormalized.Y);
                data.Write(dirNormalized.Z);
                data.Write(0.0f); // Pad
                data.Write(dirLight.color.X);
                data.Write(dirLight.color.Y);
                data.Write(dirLight.color.Z);
                data.Write(0.0f); // Pad
            }

            setUboData(DIR_LIGHT_DATA_UBO_BINDING, dirLightDataUboId, buffer.ToArray());
        }

        public void setPointLightData(byte[] pointLightData) {
            setUboData(POINT_LIGHT_DATA_UBO_BINDING, pointLightDataUboId, pointLightData);
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

        public IMesh loadMesh(byte[] vertexData, List<VertexAttribute> vertexAttributes, uint vertexSize, byte[] indexData, Type indexType) {
            return new OGLMesh(vertexData, vertexAttributes, vertexSize, indexData, indexType);
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
