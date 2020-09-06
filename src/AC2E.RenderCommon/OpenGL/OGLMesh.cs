﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Veldrid.OpenGLBinding;
using static AC2E.RenderCommon.WinOpenGL;
using static AC2E.RenderCommon.OpenGL.OGLUtil;
using static Veldrid.OpenGLBinding.OpenGLNative;

namespace AC2E.RenderCommon.OpenGL {

    internal class OGLMesh : IMesh {

        public readonly uint vaoId;
        public readonly DrawElementsType elementType;
        public readonly uint numIndices;

        public OGLMesh(byte[] vertexData, List<VertexAttribute> vertexAttributes, uint vertexSize, byte[] elementData, Type elementType) {
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
                    glVertexAttribPointer(vertexAttribute.id, (int)vertexAttribute.numComponents, TYPE_TO_VERT_ATTRIB_TYPE[vertexAttribute.componentType], GLboolean.False, vertexSize, (void*)vertexAttribute.offset);
                    checkError();
                    glEnableVertexAttribArray(vertexAttribute.id);
                    checkError();
                }

                fixed (void* elementDataPtr = elementData) {
                    glBufferData(BufferTarget.ElementArrayBuffer, (UIntPtr)elementData.Length, elementDataPtr, BufferUsageHint.StaticDraw);
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

            this.elementType = TYPE_TO_ELEMENT_TYPE[elementType];
            numIndices = (uint)elementData.Length / (uint)Marshal.SizeOf(elementType);
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
}
