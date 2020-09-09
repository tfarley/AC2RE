using AC2E.Def;
using AC2E.UICommon;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace AC2E.DatTool.UI {

    public partial class MainWindow : Window {

        private string originalTitle;

        public MainWindow() {
            InitializeComponent();

            originalTitle = Title;
        }

        private void openMenuItem_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Dat Files (*.dat)|*.dat|All files (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == true) {
                Title = $"{originalTitle} - {openFileDialog.SafeFileName}";

                DatReader datReader = new DatReader(new AC2Reader(File.OpenRead(openFileDialog.FileName)));
                // 0x1F000023 = human male, 0x1F001110 = rabbit
                new RenderPreview(datReader, new DataId(0x1F000023)).Show();
            }
        }
    }
}
