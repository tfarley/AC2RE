using System;
using System.Text;
using Veldrid.OpenGLBinding;
using static AC2E.RenderCommon.OpenGL.OGLUtil;
using static Veldrid.OpenGLBinding.OpenGLNative;

namespace AC2E.RenderCommon.OpenGL {

    internal class OGLShaderProgram : IShaderProgram {

        public static readonly string VIEW_DATA_UBO_NAME = "ViewData";
        public static readonly string AMBIENT_LIGHT_DATA_UBO_NAME = "AmbientLightData";
        public static readonly string DIR_LIGHT_DATA_UBO_NAME = "DirLightData";
        public static readonly string POINT_LIGHT_DATA_UBO_NAME = "PointLightData";

        public readonly uint id;

        public OGLShaderProgram(params IShader[] shaders) {
            id = glCreateProgram();
            checkError();

            foreach (IShader shader in shaders) {
                glAttachShader(id, ((OGLShader)shader).id);
                checkError();
            }

            glLinkProgram(id);
            checkError();

            unsafe {
                int success;
                glGetProgramiv(id, GetProgramParameterName.LinkStatus, &success);
                checkError();
                if (success == 0) {
                    byte[] infoLog = new byte[2048];
                    fixed (byte* infoLogPtr = infoLog) {
                        glGetProgramInfoLog(id, (uint)infoLog.Length, null, infoLogPtr);
                        checkError();
                    }
                    string infoLogText = Encoding.UTF8.GetString(infoLog);
                    throw new Exception(infoLogText);
                }
            }

            bindUbo(VIEW_DATA_UBO_NAME, OGLRenderer.VIEW_DATA_UBO_BINDING);
            bindUbo(AMBIENT_LIGHT_DATA_UBO_NAME, OGLRenderer.AMBIENT_LIGHT_DATA_UBO_BINDING);
            bindUbo(DIR_LIGHT_DATA_UBO_NAME, OGLRenderer.DIR_LIGHT_DATA_UBO_BINDING);
            bindUbo(POINT_LIGHT_DATA_UBO_NAME, OGLRenderer.POINT_LIGHT_DATA_UBO_BINDING);
        }

        private void bindUbo(string uboName, uint uboBinding) {
            uint uboIndex = getUboIndex(uboName);
            if (uboIndex != GL_INVALID_INDEX) {
                glUniformBlockBinding(id, uboIndex, uboBinding);
                checkError();
            }
        }

        private uint getUboIndex(string uboName) {
            int byteCount = Encoding.UTF8.GetByteCount(uboName) + 1;
            unsafe {
                byte* uboNamePtr = stackalloc byte[byteCount];
                fixed (char* charPtr = uboName) {
                    Encoding.UTF8.GetBytes(charPtr, uboName.Length, uboNamePtr, byteCount);
                }
                uboNamePtr[byteCount - 1] = 0;
                uint uboIndex = glGetUniformBlockIndex(id, uboNamePtr);
                checkError();
                return uboIndex;
            }
        }

        public void Dispose() {
            glDeleteProgram(id);
            checkError();
        }

        public void use() {
            glUseProgram(id);
            checkError();
        }
    }
}
