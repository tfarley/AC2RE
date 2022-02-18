using AC2RE.Definitions;
using AC2RE.UICommon;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AC2RE.DatTool.UI;

public partial class MainWindow : Window {

    public MainWindow() {
        InitializeComponent();
    }

    private void generateHeightmapMenuItem_Click(object sender, RoutedEventArgs e) {
        UIUtil.swallowException(() => {
            using (DatReader cell1DatReader = new(cell1DatPathTextBox.Text)) {
                var pixelFormat = PixelFormats.Rgb24;
                byte[] pixels = CellParse.generateHeightmap(cell1DatReader, pixelFormat.BitsPerPixel, out int imageWidth, out int imageHeight, out int stride);

                BitmapSource bitmap = BitmapSource.Create(imageWidth, imageHeight, 10.0, 1.0, pixelFormat, null, pixels, stride);
                new HeightmapWindow(bitmap).Show();
            }
        });
    }

    private void promptForDatFile(TextBox textBox) {
        OpenFileDialog openFileDialog = new();
        openFileDialog.Filter = "Dat Files (*.dat)|*.dat|All files (*.*)|*.*";
        openFileDialog.RestoreDirectory = true;
        if (openFileDialog.ShowDialog() == true) {
            textBox.Text = openFileDialog.FileName;
            textBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }
    }

    private void portalDatPathBrowseButton_Click(object sender, RoutedEventArgs e) {
        promptForDatFile(portalDatPathTextBox);
    }

    private void highresDatPathBrowseButton_Click(object sender, RoutedEventArgs e) {
        promptForDatFile(highresDatPathTextBox);
    }

    private void cell1DatPathBrowseButton_Click(object sender, RoutedEventArgs e) {
        promptForDatFile(cell1DatPathTextBox);
    }

    private void localDatPathBrowseButton_Click(object sender, RoutedEventArgs e) {
        promptForDatFile(localDatPathTextBox);
    }

    private void extractButton_Click(object sender, RoutedEventArgs e) {
        new ExtractWindow(portalDatPathTextBox, highresDatPathTextBox, cell1DatPathTextBox, localDatPathTextBox).Show();
    }

    private void inspectorButton_Click(object sender, RoutedEventArgs e) {

    }

    private void modelViewerButton_Click(object sender, RoutedEventArgs e) {
        UIUtil.swallowException(() => {
            DatReader datReader = new(portalDatPathTextBox.Text);
            // 0x1F000023 = human male, 0x1F001110 = rabbit
            new ModelViewerWindow(datReader, new(0x1F000023)).Show();
        });
    }

    private void mapViewerButton_Click(object sender, RoutedEventArgs e) {

    }
}
