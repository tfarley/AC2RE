using System;
using System.Text;
using Veldrid.OpenGLBinding;
using static AC2RE.RenderLib.OpenGL.OGLUtil;
using static Veldrid.OpenGLBinding.OpenGLNative;

namespace AC2RE.RenderLib.OpenGL {

    internal class OGLShader : IShader {

        public readonly uint id;

        public OGLShader(byte[] shaderSource, ShaderType shaderType) {
            id = glCreateShader(shaderType);
            checkError();

            unsafe {
                int shaderSourceLen = shaderSource.Length;
                fixed (byte* shaderSourcePtr = shaderSource) {
                    glShaderSource(id, 1, &shaderSourcePtr, &shaderSourceLen);
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
                    throw new Exception(infoLogText);
                }
            }
        }

        public void Dispose() {
            glDeleteShader(id);
            checkError();
        }
    }
}
