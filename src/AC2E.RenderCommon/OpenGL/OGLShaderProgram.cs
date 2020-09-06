using System.Text;
using Veldrid.OpenGLBinding;
using static AC2E.RenderCommon.OpenGL.OGLUtil;
using static Veldrid.OpenGLBinding.OpenGLNative;

namespace AC2E.RenderCommon.OpenGL {

    internal class OGLShaderProgram : IShaderProgram {

        public static readonly string VIEW_DATA_UBO_NAME = "ViewData";
        public static readonly string LIGHT_DATA_UBO_NAME = "LightData";

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
                    System.Console.WriteLine(infoLogText);
                }
            }

            uint viewDataUboIndex = getUboIndex(VIEW_DATA_UBO_NAME);
            if (viewDataUboIndex != GL_INVALID_INDEX) {
                glUniformBlockBinding(id, viewDataUboIndex, WinOGLRenderer.VIEW_DATA_UBO_BINDING);
                checkError();
            }

            uint lightDataUboIndex = getUboIndex(LIGHT_DATA_UBO_NAME);
            if (lightDataUboIndex != GL_INVALID_INDEX) {
                glUniformBlockBinding(id, lightDataUboIndex, WinOGLRenderer.LIGHT_DATA_UBO_BINDING);
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
