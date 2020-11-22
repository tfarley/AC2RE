using System;
using System.Collections.Generic;
using Veldrid.OpenGLBinding;
using static AC2RE.RenderLib.OpenGL.OGLUtil;
using static Veldrid.OpenGLBinding.OpenGLNative;

namespace AC2RE.RenderLib.OpenGL {

    internal class OGLTexture : ITexture {

        private static readonly Dictionary<TextureFormat, Tuple<GLPixelFormat, PixelInternalFormat>> TEXTURE_TO_OPENGL_FORMATS = new() {
            { TextureFormat.R8G8B8A8, new(GLPixelFormat.Rgba, PixelInternalFormat.Rgba) },
            { TextureFormat.DXT1, new(GLPixelFormat.Rgba, PixelInternalFormat.CompressedRgbaS3tcDxt1Ext) },
            { TextureFormat.DXT3, new(GLPixelFormat.Rgba, PixelInternalFormat.CompressedRgbaS3tcDxt3Ext) },
            { TextureFormat.DXT5, new(GLPixelFormat.Rgba, PixelInternalFormat.CompressedRgbaS3tcDxt5Ext) },
        };

        public readonly uint id;

        public OGLTexture(byte[] textureData, uint width, uint height, TextureFormat format) {
            glGenTextures(1, out id);
            checkError();

            glActiveTexture(TextureUnit.Texture0);
            checkError();

            glBindTexture(TextureTarget.Texture2D, id);
            checkError();

            glTexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            checkError();

            glTexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            checkError();

            glTexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            checkError();

            glTexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            checkError();

            (GLPixelFormat pixelFormat, PixelInternalFormat internalFormat) = TEXTURE_TO_OPENGL_FORMATS[format];

            unsafe {
                switch (format) {
                    case TextureFormat.R8G8B8A8: {
                            glTexImage2D(TextureTarget.Texture2D, 0, internalFormat, width, height, 0, pixelFormat, GLPixelType.UnsignedByte, null);
                            checkError();
                            break;
                        }
                    case TextureFormat.DXT1:
                    case TextureFormat.DXT3:
                    case TextureFormat.DXT5: {
                            // No binding for glCompressedTexImage2D, so reserve space in the correct format and then update it with real data
                            glTexImage2D(TextureTarget.Texture2D, 0, internalFormat, width, height, 0, pixelFormat, GLPixelType.UnsignedByte, null);
                            checkError();

                            fixed (void* textureDataPtr = textureData) {
                                glCompressedTexSubImage2D(TextureTarget.Texture2D, 0, 0, 0, width, height, internalFormat, (uint)textureData.Length, textureDataPtr);
                                checkError();
                            }
                            break;
                        }
                }
            }

            glGenerateMipmap(TextureTarget.Texture2D);
            checkError();
        }

        public void Dispose() {
            uint tempId = id;
            glDeleteTextures(1, ref tempId);
            checkError();
        }

        public void bind(int unit) {
            glActiveTexture((TextureUnit)((uint)TextureUnit.Texture0 + unit));
            checkError();

            glBindTexture(TextureTarget.Texture2D, id);
            checkError();
        }
    }
}
