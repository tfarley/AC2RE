using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace AC2RE.UICommon;

public static class UIUtil {

    public static string? promptSelectDirectory() {
        SaveFileDialog saveFileDialog = new();
        saveFileDialog.FileName = "dummy";
        saveFileDialog.Filter = "Directory|directory";
        saveFileDialog.RestoreDirectory = true;
        if (saveFileDialog.ShowDialog() == true) {
            return Path.GetDirectoryName(saveFileDialog.FileName);
        }

        return null;
    }

    public static void swallowException(Action action) {
        try {
            action.Invoke();
        } catch (Exception e) {
            Debugger.Break();
            MessageBox.Show(e.ToString(), "Unhandled exception", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
