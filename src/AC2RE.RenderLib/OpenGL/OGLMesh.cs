using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Veldrid.OpenGLBinding;
using static AC2RE.RenderLib.OpenGL.OGLUtil;
using static AC2RE.RenderLib.WinOpenGL;
using static Veldrid.OpenGLBinding.OpenGLNative;

namespace AC2RE.RenderLib.OpenGL;

internal class OGLMesh : IMesh {

    public readonly uint vaoId;
    public readonly DrawElementsType elementType;
    public readonly uint numIndices;

    public OGLMesh(byte[] vertexData, List<VertexAttribute> vertexAttributes, uint vertexSize, byte[] indexData, Type indexType) {
        glGenVertexArrays(1, out vaoId);
        checkError();

        glBindVertexArray(vaoId);
        checkError();

        glGenBuffers(1, out uint vboId);
        checkError();

        glBindBuffer(BufferTarget.ArrayBuffer, vboId);
        checkError();

        glGenBuffers(1, out uint eboId);
        checkError();

        glBindBuffer(BufferTarget.ElementArrayBuffer, eboId);
        checkError();

        unsafe {
            int maxVertexAttribs;
            glGetIntegerv(GetPName.MaxVertexAttribs, &maxVertexAttribs);

            fixed (void* vertexDataPtr = vertexData) {
                glBufferData(BufferTarget.ArrayBuffer, (UIntPtr)vertexData.Length, vertexDataPtr, BufferUsageHint.StaticDraw);
                checkError();
            }
            if (vertexSize == 0) {
                foreach (VertexAttribute vertexAttribute in vertexAttributes) {
                    vertexSize += vertexAttribute.numComponents * (uint)Marshal.SizeOf(vertexAttribute.componentType);
                }
            }
            foreach (VertexAttribute vertexAttribute in vertexAttributes) {
                if (vertexAttribute.id > maxVertexAttribs) {
                    throw new IndexOutOfRangeException($"Vertex attribute with id {vertexAttribute.id} exceeds max of {maxVertexAttribs}.");
                }

                glVertexAttribPointer(vertexAttribute.id, (int)vertexAttribute.numComponents, TYPE_TO_VERT_ATTRIB_TYPE[vertexAttribute.componentType], vertexAttribute.normalize ? GLboolean.True : GLboolean.False, vertexSize, (void*)vertexAttribute.offset);
                checkError();
                glEnableVertexAttribArray(vertexAttribute.id);
                checkError();
            }

            fixed (void* indexDataPtr = indexData) {
                glBufferData(BufferTarget.ElementArrayBuffer, (UIntPtr)indexData.Length, indexDataPtr, BufferUsageHint.StaticDraw);
                checkError();
            }
        }

        // Unbind before deleting buffers or else it will actually unbind them from VAO itself
        glBindVertexArray(0);
        checkError();

        glDeleteBuffers(1, ref vboId);
        checkError();

        glDeleteBuffers(1, ref eboId);
        checkError();

        elementType = TYPE_TO_ELEMENT_TYPE[indexType];
        numIndices = (uint)indexData.Length / (uint)Marshal.SizeOf(indexType);
    }

    public void Dispose() {
        uint tempVaoId = vaoId;
        glDeleteVertexArrays(1, ref tempVaoId);
        checkError();
    }

    public void draw() {
        glBindVertexArray(vaoId);
        checkError();

        unsafe {
            glDrawElements(PrimitiveType.Triangles, numIndices, elementType, (void*)0);
            checkError();
        }
    }
}
