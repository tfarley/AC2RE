using AC2RE.Definitions;
using AC2RE.UICommon.UI;
using AC2RE.Utils;
using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace AC2RE.DatTool.UI;

public partial class MainWindow : Window {

    private string originalTitle;

    public MainWindow() {
        InitializeComponent();

        originalTitle = Title;

        Loaded += (_, _) => {

            /*
            using (DatReader datReader = new("G:\\Asheron's Call 2\\portal.dat_server")) {
                //DatParse.parseDat(DbTypeDef.DatType.PORTAL, datReader, "portalparsed", DbType.WSTATE);

                MasterProperty.loadMasterProperties(datReader);
                PackageManager.loadPackageTypes(datReader);

                //Dictionary<PackageType, List<DataId>> packageTypeToDids = DatParse.getWeeniePackageTypes(datReader);
                //File.WriteAllText("weeniePackageTypes.txt", Util.objectToString(packageTypeToDids));

                //using (DatReader localDatReader = new("G:\\Asheron's Call 2\\local_English.dat")) {
                //    Dictionary<DataId, string> monsterDidToName = DatParse.getMonsterNames(datReader, localDatReader);
                //    File.WriteAllText("monsterNames.txt", Util.objectToString(monsterDidToName));
                //}

                //Dictionary<SkillId, DataId> skillIdToDid = new();
                //Dictionary<SkillId, Skill> skillIdToSkill = new();
                //DatParse.getSkills(datReader, skillIdToDid, skillIdToSkill);
                //File.WriteAllText("skills.txt", Util.objectToString(skillIdToDid) + '\n' + Util.objectToString(skillIdToSkill));

                //File.WriteAllText("effects.txt", Util.objectToString(DatParse.getEffects(datReader)));
            }
            */


            /*
            using (DatReader datReader = new("G:\\Asheron's Call 2\\portal.dat_server")) {
                MasterProperty.loadMasterProperties(datReader);
            }
            using (DatReader datReader = new("G:\\Asheron's Call 2\\cell_1.dat")) {
                DatParse.parseDat(DbTypeDef.DatType.CELL, datReader, "cell1parsed", DbType.LANDBLOCKDATA);
                //new MapWindow(datReader).Show();
                //CellParse.getMissingCells(datReader);
            }
            */

            /*
            using (DatReader datReader = new("G:\\Asheron's Call 2\\cell_2.dat")) {
                DatParse.parseDat(DbTypeDef.DatType.CELL, datReader, "cell2parsed", DbType.DATFILEDATA);
            }
            */

            /*
            using (DatReader datReader = new("G:\\Asheron's Call 2\\highres.dat")) {
                DatParse.parseDat(DbTypeDef.DatType.PORTAL, datReader, "highresparsed", DbType.DATFILEDATA);
            }
            */

            /*
            using (DatReader datReader = new("G:\\Asheron's Call 2\\local_English.dat")) {
                DatParse.parseDat(DbTypeDef.DatType.LOCAL, datReader, "localenglishparsed", DbType.ENCODED_WAV);
            }
            */
        };
    }

    private void openMenuItem_Click(object sender, RoutedEventArgs e) {
        OpenFileDialog openFileDialog = new();
        openFileDialog.Filter = "Dat Files (*.dat)|*.dat|All files (*.*)|*.*";
        openFileDialog.RestoreDirectory = true;
        if (openFileDialog.ShowDialog() == true) {
            Title = $"{originalTitle} - {openFileDialog.SafeFileName}";

            DatReader datReader = new(new AC2Reader(File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read, FileShare.Read)));
            // 0x1F000023 = human male, 0x1F001110 = rabbit
            new RenderPreviewWindow(datReader, new(0x1F000023)).Show();
        }
    }
}
