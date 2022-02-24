using AC2RE.Definitions;
using AC2RE.UICommon;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace AC2RE.DatTool.UI;

public partial class ExtractWindow : Window {

    private readonly TextBox portalDatPathTextBox;
    private readonly TextBox highresDatPathTextBox;
    private readonly TextBox cell1DatPathTextBox;
    private readonly TextBox localDatPathTextBox;

    private readonly Dictionary<DbType, CheckBox> dbTypeToCheckBox = new();

    private bool suppressSave;

    public ExtractWindow(TextBox portalDatPathTextBox, TextBox highresDatPathTextBox, TextBox cell1DatPathTextBox, TextBox localDatPathTextBox) {
        InitializeComponent();

        this.portalDatPathTextBox = portalDatPathTextBox;
        this.highresDatPathTextBox = highresDatPathTextBox;
        this.cell1DatPathTextBox = cell1DatPathTextBox;
        this.localDatPathTextBox = localDatPathTextBox;

        HashSet<DbType> selectedTypes = new(UserSettings.extractSelections);

        StackPanel? curDbTypeContainer = null;
        List<DbType> dbTypes = new(DbTypeDef.TYPE_TO_DEF.Keys);
        dbTypes.Sort((a, b) => a.ToString().CompareTo(b.ToString()));
        for (int i = 0; i < dbTypes.Count; i++) {
            if (i % 18 == 0) {
                curDbTypeContainer = new StackPanel();
                dbTypeCheckBoxesPanel.Children.Add(curDbTypeContainer);
            }
            DbType dbType = dbTypes[i];
            DbTypeDef dbTypeDef = DbTypeDef.TYPE_TO_DEF[dbType];
            CheckBox checkBox = new();
            checkBox.Content = dbType.ToString();
            checkBox.IsChecked = selectedTypes.Contains(dbType);
            checkBox.Checked += (_, _) => saveSelections();
            checkBox.Unchecked += (_, _) => saveSelections();
            curDbTypeContainer!.Children.Add(checkBox);
            dbTypeToCheckBox[dbType] = checkBox;
        }
    }

    private DbType[] getSelectedDbTypes() {
        List<DbType> selectedDbTypes = new();
        foreach ((DbType dbType, CheckBox dbTypeCheckBox) in dbTypeToCheckBox) {
            if (dbTypeCheckBox.IsChecked == true) {
                selectedDbTypes.Add(dbType);
            }
        }

        return selectedDbTypes.ToArray();
    }

    private void saveSelections() {
        if (suppressSave) {
            return;
        }

        UserSettings.extractSelections = getSelectedDbTypes();
    }

    private void toggleAllButton_Click(object sender, RoutedEventArgs e) {
        UIUtil.swallowException(() => {
            bool anyUnchecked = false;
            foreach (CheckBox dbTypeCheckBox in dbTypeToCheckBox.Values) {
                if (dbTypeCheckBox.IsChecked != true) {
                    anyUnchecked = true;
                    break;
                }
            }
            suppressSave = true;
            foreach (CheckBox dbTypeCheckBox in dbTypeToCheckBox.Values) {
                dbTypeCheckBox.IsChecked = anyUnchecked;
            }
            suppressSave = false;
            saveSelections();
        });
    }

    private void parseOnlyButton_Click(object sender, RoutedEventArgs e) {
        UIUtil.swallowException(() => {
            extract(null);
        });
    }

    private void extractButton_Click(object sender, RoutedEventArgs e) {
        UIUtil.swallowException(() => {
            string? outputPath = UIUtil.promptSelectDirectory();
            if (outputPath != null) {
                extract(outputPath);
            }
        });
    }

    private void extract(string? outputPath) {
        DbType[] selectedDbTypes = getSelectedDbTypes();

        UIUtil.swallowException(() => {
            using (DatReader portalDatReader = new(portalDatPathTextBox.Text)) {
                MasterProperty.loadMasterProperties(portalDatReader);
                PackageTypes.loadPackageTypes(portalDatReader);

                DatParse.parseDat(DbTypeDef.DatType.PORTAL, portalDatReader, outputPath, selectedDbTypes);
            }
        });

        UIUtil.swallowException(() => {
            using (DatReader cell1DatReader = new(cell1DatPathTextBox.Text)) {
                DatParse.parseDat(DbTypeDef.DatType.CELL, cell1DatReader, outputPath, selectedDbTypes);
            }
        });

        UIUtil.swallowException(() => {
            using (DatReader localDatReader = new(localDatPathTextBox.Text)) {
                DatParse.parseDat(DbTypeDef.DatType.LOCAL, localDatReader, outputPath, selectedDbTypes);
            }
        });
    }
}
