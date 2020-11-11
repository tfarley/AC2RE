using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace AC2E.UICommon {

    public class HwndElement : HwndHost {

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr SetParent(IntPtr hWnd, IntPtr hWndParent);

        private const int GWL_STYLE = -16;
        private const int WS_CHILD = 0x40000000;

        Window window;
        public IntPtr hwnd { get; init; }

        public HwndElement() {
            window = new();
            window.Show();

            hwnd = new WindowInteropHelper(window).Handle;

            SetWindowLong(hwnd, GWL_STYLE, WS_CHILD);
        }

        protected override HandleRef BuildWindowCore(HandleRef hwndParent) {
            SetParent(hwnd, hwndParent.Handle);
            return new(this, hwnd);
        }

        protected override void DestroyWindowCore(HandleRef hwnd) {
            window.Close();
        }
    }
}
