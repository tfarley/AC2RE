using AC2RE.UICommon;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace AC2RE.DatTool.UI;

public partial class HeightmapWindow : Window {

    public HeightmapWindow(BitmapSource heightmapBitmap) {
        InitializeComponent();

        mapImage.Source = heightmapBitmap;
    }

    private void saveMenuItem_Click(object sender, RoutedEventArgs e) {
        UIUtil.swallowException(() => {
            SaveFileDialog saveFileDialog = new();
            saveFileDialog.Filter = "Png Files (*.png)|*.png|All files (*.*)|*.*";
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == true) {
                using (Stream output = File.Open(saveFileDialog.FileName, FileMode.Create, FileAccess.Write, FileShare.Read)) {
                    BitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)mapImage.Source));
                    encoder.Save(output);
                }
            }
        });
    }
}
