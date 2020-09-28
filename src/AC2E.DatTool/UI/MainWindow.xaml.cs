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

            Loaded += (sender, e) => {
                /*
                using (DatReader datReader = new DatReader("G:\\Asheron's Call 2\\portal.dat_server")) {
                    DatParse.parseDat(datReader, "portalparsed", DbType.WSTATE);
                }
                */

                /*
                using (DatReader datReader = new DatReader("G:\\Asheron's Call 2\\cell_1.dat")) {
                    //DatParse.parseDat(datReader, "cell1parsed", DbType.ENVCELL);
                    CellParse.getMissingCells(datReader);
                }
                */

                /*
                using (DatReader datReader = new DatReader("G:\\Asheron's Call 2\\cell_2.dat")) {
                    DatParse.parseDat(datReader, "cell2parsed", DbType.DATFILEDATA);
                }
                */

                /*
                using (DatReader datReader = new DatReader("G:\\Asheron's Call 2\\highres.dat")) {
                    DatParse.parseDat(datReader, "highresparsed", DbType.DATFILEDATA);
                }
                */

                /*
                using (DatReader datReader = new DatReader("G:\\Asheron's Call 2\\local_English.dat")) {
                    DatParse.parseDat(datReader, "localenglishparsed", DbType.ENCODED_WAV);
                }
                */

                /*
                using (DatReader datReader = new DatReader("G:\\Asheron's Call 2\\local_English.dat")) {
                    DatParse.parseDat(datReader, "localenglishparsed", DbType.ENCODED_WAV);
                }
                */
            };
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
