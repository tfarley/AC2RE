using System;
using System.Diagnostics;
using System.Windows;

namespace AC2RE.UICommon {

    public static class UIUtil {

        public static void swallowException(Action action) {
            try {
                action.Invoke();
            } catch (Exception ex) {
                Debugger.Break();
                MessageBox.Show(ex.ToString(), "Unhandled exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
