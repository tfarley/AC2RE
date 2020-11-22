using System;
using System.Runtime.InteropServices;

namespace AC2RE.RenderLib {

    internal static class WinKernel {

        [Flags]
        public enum LoadLibraryFlags : uint {
            None = 0,
            DONT_RESOLVE_DLL_REFERENCES = 0x00000001,
            LOAD_IGNORE_CODE_AUTHZ_LEVEL = 0x00000010,
            LOAD_LIBRARY_AS_DATAFILE = 0x00000002,
            LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = 0x00000040,
            LOAD_LIBRARY_AS_IMAGE_RESOURCE = 0x00000020,
            LOAD_LIBRARY_SEARCH_APPLICATION_DIR = 0x00000200,
            LOAD_LIBRARY_SEARCH_DEFAULT_DIRS = 0x00001000,
            LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR = 0x00000100,
            LOAD_LIBRARY_SEARCH_SYSTEM32 = 0x00000800,
            LOAD_LIBRARY_SEARCH_USER_DIRS = 0x00000400,
            LOAD_WITH_ALTERED_SEARCH_PATH = 0x00000008
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hReservedNull, LoadLibraryFlags dwFlags);

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);
    }

    internal static class WinUser {

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);
    }

    internal static class WinGDI {

        [StructLayout(LayoutKind.Sequential)]
        public struct PIXELFORMATDESCRIPTOR {
            public ushort nSize;
            public ushort nVersion;
            public PFD_FLAGS dwFlags;
            public PFD_PIXEL_TYPE iPixelType;
            public byte cColorBits;
            public byte cRedBits;
            public byte cRedShift;
            public byte cGreenBits;
            public byte cGreenShift;
            public byte cBlueBits;
            public byte cBlueShift;
            public byte cAlphaBits;
            public byte cAlphaShift;
            public byte cAccumBits;
            public byte cAccumRedBits;
            public byte cAccumGreenBits;
            public byte cAccumBlueBits;
            public byte cAccumAlphaBits;
            public byte cDepthBits;
            public byte cStencilBits;
            public byte cAuxBuffers;
            public PFD_LAYER_TYPES iLayerType;
            public byte bReserved;
            public uint dwLayerMask;
            public uint dwVisibleMask;
            public uint dwDamageMask;
        }

        [Flags]
        public enum PFD_FLAGS : uint {
            PFD_DOUBLEBUFFER = 0x00000001,
            PFD_STEREO = 0x00000002,
            PFD_DRAW_TO_WINDOW = 0x00000004,
            PFD_DRAW_TO_BITMAP = 0x00000008,
            PFD_SUPPORT_GDI = 0x00000010,
            PFD_SUPPORT_OPENGL = 0x00000020,
            PFD_GENERIC_FORMAT = 0x00000040,
            PFD_NEED_PALETTE = 0x00000080,
            PFD_NEED_SYSTEM_PALETTE = 0x00000100,
            PFD_SWAP_EXCHANGE = 0x00000200,
            PFD_SWAP_COPY = 0x00000400,
            PFD_SWAP_LAYER_BUFFERS = 0x00000800,
            PFD_GENERIC_ACCELERATED = 0x00001000,
            PFD_SUPPORT_DIRECTDRAW = 0x00002000,
            PFD_DIRECT3D_ACCELERATED = 0x00004000,
            PFD_SUPPORT_COMPOSITION = 0x00008000,
            PFD_DEPTH_DONTCARE = 0x20000000,
            PFD_DOUBLEBUFFER_DONTCARE = 0x40000000,
            PFD_STEREO_DONTCARE = 0x80000000
        }

        public enum PFD_LAYER_TYPES : byte {
            PFD_MAIN_PLANE = 0,
            PFD_OVERLAY_PLANE = 1,
            PFD_UNDERLAY_PLANE = 255
        }

        public enum PFD_PIXEL_TYPE : byte {
            PFD_TYPE_RGBA = 0,
            PFD_TYPE_COLORINDEX = 1
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int ChoosePixelFormat(IntPtr hdc, [In] ref PIXELFORMATDESCRIPTOR ppfd);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern bool SetPixelFormat(IntPtr hdc, int iPixelFormat, ref PIXELFORMATDESCRIPTOR ppfd);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern bool SwapBuffers(IntPtr hdc);
    }

    internal static class WinOpenGL {

        [DllImport("opengl32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr wglCreateContext(IntPtr hdc);

        [DllImport("opengl32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern bool wglDeleteContext(IntPtr hglrc);

        [DllImport("opengl32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern bool wglMakeCurrent(IntPtr hdc, IntPtr hglrc);

        [DllImport("opengl32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr wglGetProcAddress(string procName);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        private delegate void glDeleteVertexArrays_t(uint n, ref uint buffers);
        private static glDeleteVertexArrays_t? p_glDeleteVertexArrays;
        public static void glDeleteVertexArrays(uint n, ref uint vertexArrays) => p_glDeleteVertexArrays!(n, ref vertexArrays);

        public static void LoadAllFunctions(Func<string, IntPtr> getProcAddress) {
            LoadFunction("glDeleteVertexArrays", out p_glDeleteVertexArrays, getProcAddress);
        }

        private static void LoadFunction<T>(string name, out T? field, Func<string, IntPtr> getProcAddress) where T : class {
            IntPtr funcPtr = getProcAddress(name);
            if (funcPtr != IntPtr.Zero) {
                field = Marshal.GetDelegateForFunctionPointer<T>(funcPtr);
            } else {
                field = default;
            }
        }
    }
}
