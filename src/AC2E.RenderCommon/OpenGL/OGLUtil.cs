using System;
using System.Collections.Generic;
using System.Diagnostics;
using Veldrid.OpenGLBinding;
using static Veldrid.OpenGLBinding.OpenGLNative;

namespace AC2E.RenderCommon.OpenGL {

    internal static class OGLUtil {

        public static readonly uint GL_INVALID_INDEX = 0xFFFFFFFF;

        public static readonly Dictionary<Type, VertexAttribPointerType> TYPE_TO_VERT_ATTRIB_TYPE = new Dictionary<Type, VertexAttribPointerType> {
            { typeof(byte), VertexAttribPointerType.UnsignedByte },
            { typeof(short), VertexAttribPointerType.Short },
            { typeof(ushort), VertexAttribPointerType.UnsignedShort },
            { typeof(int), VertexAttribPointerType.Int },
            { typeof(uint), VertexAttribPointerType.UnsignedInt },
            { typeof(float), VertexAttribPointerType.Float },
            { typeof(double), VertexAttribPointerType.Double },
        };

        public static readonly Dictionary<Type, DrawElementsType> TYPE_TO_ELEMENT_TYPE = new Dictionary<Type, DrawElementsType> {
            { typeof(byte), DrawElementsType.UnsignedByte },
            { typeof(short), DrawElementsType.UnsignedShort },
            { typeof(ushort), DrawElementsType.UnsignedShort },
            { typeof(int), DrawElementsType.UnsignedInt },
            { typeof(uint), DrawElementsType.UnsignedInt },
        };

        [Conditional("DEBUG")]
        public static void checkError() {
            uint error = glGetError();
            if (error != 0) {
                throw new Exception("glGetError: " + (ErrorCode)error);
            }
        }
    }
}
