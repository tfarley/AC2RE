using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using static AC2RE.RenderLib.WinGDI;
using static AC2RE.RenderLib.WinKernel;
using static AC2RE.RenderLib.WinOpenGL;
using static AC2RE.RenderLib.WinUser;
using static Veldrid.OpenGLBinding.OpenGLNative;

namespace AC2RE.RenderLib.OpenGL {

    internal class WinOGLRenderer : OGLRenderer {

        private readonly IntPtr openglModule;
        private readonly IntPtr hwnd;
        private readonly IntPtr hdc;
        private readonly IntPtr hglrc;

        public WinOGLRenderer(IntPtr hwnd) {
            if ((openglModule = LoadLibraryEx("opengl32.dll", IntPtr.Zero, LoadLibraryFlags.LOAD_LIBRARY_SEARCH_SYSTEM32)) == IntPtr.Zero) {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            this.hwnd = hwnd;
            hdc = GetDC(hwnd);

            PIXELFORMATDESCRIPTOR pixelFormatDescriptor = new() {
                nSize = (ushort)Marshal.SizeOf(typeof(PIXELFORMATDESCRIPTOR)),
                nVersion = 1,
                dwFlags = PFD_FLAGS.PFD_DRAW_TO_WINDOW | PFD_FLAGS.PFD_SUPPORT_OPENGL | PFD_FLAGS.PFD_DOUBLEBUFFER | PFD_FLAGS.PFD_SUPPORT_COMPOSITION,
                iPixelType = PFD_PIXEL_TYPE.PFD_TYPE_RGBA,
                cColorBits = 24,
                cDepthBits = 32,
                iLayerType = PFD_LAYER_TYPES.PFD_MAIN_PLANE,
            };

            int pixelFormat = ChoosePixelFormat(hdc, ref pixelFormatDescriptor);
            if (!SetPixelFormat(hdc, pixelFormat, ref pixelFormatDescriptor)) {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            if ((hglrc = wglCreateContext(hdc)) == IntPtr.Zero) {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            if (!wglMakeCurrent(hdc, hglrc)) {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            LoadAllFunctions(hglrc, getProcAddress, false);
            LoadAllFunctions(getProcAddress);

            init();
        }

        private IntPtr getProcAddress(string procName) {
            IntPtr procAddr = GetProcAddress(openglModule, procName);
            if (procAddr == IntPtr.Zero) {
                procAddr = wglGetProcAddress(procName);
            }
            return procAddr;
        }

        public override void Dispose() {
            base.Dispose();

            if (!wglDeleteContext(hglrc)) {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        public override void swap() {
            SwapBuffers(hdc);
        }
    }
}
