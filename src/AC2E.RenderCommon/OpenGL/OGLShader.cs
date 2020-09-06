using System.Text;
using Veldrid.OpenGLBinding;
using static AC2E.RenderCommon.OpenGL.OGLUtil;
using static Veldrid.OpenGLBinding.OpenGLNative;

namespace AC2E.RenderCommon.OpenGL {

    internal class OGLShader : IShader {

        public readonly uint id;

        public OGLShader(byte[] shaderSource, ShaderType shaderType) {
            id = glCreateShader(shaderType);
            checkError();

            unsafe {
                fixed (byte* shaderSourcePtr = shaderSource) {
                    glShaderSource(id, 1, &shaderSourcePtr, null);
                    checkError();
                }
            }

            glCompileShader(id);
            checkError();

            unsafe {
                int success;
                glGetShaderiv(id, ShaderParameter.CompileStatus, &success);
                checkError();
                if (success == 0) {
                    byte[] infoLog = new byte[2048];
                    fixed (byte* infoLogPtr = infoLog) {
                        glGetShaderInfoLog(id, (uint)infoLog.Length, null, infoLogPtr);
                        checkError();
                    }
                    string infoLogText = Encoding.UTF8.GetString(infoLog);
                    System.Console.WriteLine(infoLogText);
                }
            }
        }

        public void Dispose() {
            glDeleteShader(id);
            checkError();
        }
    }
}
