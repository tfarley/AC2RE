using AC2RE.UICommon;
using AC2RE.Utils;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace AC2RE.PacketTool.UI;

public partial class MainWindow : Window {

    private readonly string originalTitle;
    private readonly ListViewSortManager recordsListViewSortManager;
    private readonly ListViewExtraColumnManager recordsListViewExtraColumnManager;

    private string? curFileName;
    private readonly List<NetBlobRow> netBlobRows = new();

    public MainWindow() {
        InitializeComponent();

        originalTitle = Title;

        recordsListViewSortManager = new(recordsListView, "lineNum");
        recordsListViewExtraColumnManager = new(recordsListView);

        foreach (MessageErrorType? messageErrorType in Enum.GetValues(typeof(MessageErrorType))) {
            if (messageErrorType != MessageErrorType.UNDETERMINED) {
                errorsFilterComboBox.Items.Add(new ComboBoxItem { Content = messageErrorType });
            }
        }

        foreach (string customFilterName in CustomFilter.FILTERS.Keys) {
            customFilterComboBox.Items.Add(new ComboBoxItem { Content = customFilterName });
        }
    }

    private void refreshItems() {
        object prevSelectedItem = recordsListView.SelectedItem;

        recordsListView.Items.Clear();

        string opcodeFilter = opcodeFilterTextBox.Text;
        string eventFilter = eventFilterTextBox.Text;
        string? stringFilter = stringFilterHexCheckBox.IsChecked != true ? stringFilterTextBox.Text : null;
        byte[]? bytePatternFilter = stringFilterHexCheckBox.IsChecked == true ? Util.hexStringToBytes(stringFilterTextBox.Text) : null;
        object? errorsFilter = ((ComboBoxItem?)errorsFilterComboBox.SelectedItem)?.Content;
        bool showIncomplete = showIncompleteCheckBox.IsChecked ?? false;
        string? customFilterName = (string?)((ComboBoxItem?)customFilterComboBox.SelectedItem)?.Content;
        CustomFilter? customFilter = customFilterName != null && !"".Equals(customFilterName) ? CustomFilter.FILTERS[customFilterName].Invoke() : null;

        recordsListViewExtraColumnManager.setExtraColumns(customFilter?.resultColumns);

        bool reselect = false;
        foreach (NetBlobRow netBlobRow in netBlobRows) {
            if (netBlobRow.matches(opcodeFilter, eventFilter, errorsFilter, stringFilter, bytePatternFilter, showIncomplete, customFilter)) {
                if (netBlobRow == prevSelectedItem) {
                    reselect = true;
                }

                recordsListView.Items.Add(netBlobRow);
            }
        }

        if (reselect) {
            recordsListView.SelectedItem = prevSelectedItem;
            recordsListView.ScrollIntoView(recordsListView.SelectedItem);
        }
    }

    private void openMenuItem_Click(object sender, RoutedEventArgs e) {
        UIUtil.swallowException(() => {
            OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "Packet Captures (*.pcap;*.pcapng)|*.pcap;*.pcapng|All files (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == true) {
                openFile(openFileDialog.FileName);
            }
        });
    }

    public void openFile(string fileName) {
        if (fileName == curFileName) {
            return;
        }

        Title = $"{originalTitle} - {Path.GetFileName(fileName)}";

        using (FileStream fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read)) {
            NetBlobCollection netBlobCollection = PcapReader.read(fileStream);

            netBlobRows.Clear();

            int lineNum = 1;
            foreach (NetBlobRecord netBlobRecord in netBlobCollection.records) {
                NetBlobRow netBlobRow = new(lineNum, netBlobRecord);
                netBlobRows.Add(netBlobRow);
                recordsListView.Items.Add(netBlobRow);
                lineNum++;
            }

            refreshItems();
        }

        curFileName = fileName;
    }

    private void searchMenuItem_Click(object sender, RoutedEventArgs e) {
        UIUtil.swallowException(() => {
            new SearchDialog(this).Show();
        });
    }

    private void toolsMenuItem_Click(object sender, RoutedEventArgs e) {
        UIUtil.swallowException(() => {
            new ToolsDialog(this).Show();
        });
    }

    private void exportJsonMenuItem_Click(object sender, RoutedEventArgs e) {
        UIUtil.swallowException(() => {

        });
    }

    private void goToSelectedButton_Click(object sender, RoutedEventArgs e) {
        UIUtil.swallowException(() => {
            if (recordsListView.SelectedItem != null) {
                recordsListView.ScrollIntoView(recordsListView.SelectedItem);
            }
        });
    }

    private void goToLineButton_Click(object sender, RoutedEventArgs e) {
        UIUtil.swallowException(() => {
            if (int.TryParse(goToLineTextBox.Text, out int targetLine)) {
                goToLine(targetLine);
            }
        });
    }

    public void goToLine(int targetLine, bool select = false) {
        NetBlobRow netBlobRow = netBlobRows[Math.Clamp(targetLine, 1, netBlobRows.Count) - 1];

        if (select) {
            recordsListView.SelectedItem = netBlobRow;
        }

        recordsListView.ScrollIntoView(netBlobRow);
    }

    private void applyFiltersButton_Click(object sender, RoutedEventArgs e) {
        UIUtil.swallowException(() => {
            refreshItems();
        });
    }

    private void recordsListViewColumnHeader_Click(object sender, RoutedEventArgs e) {
        UIUtil.swallowException(() => {
            recordsListViewSortManager.columnHeaderClicked(sender, e);
        });
    }

    private void recordsListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
        UIUtil.swallowException(() => {
            if (recordsListView.SelectedItems.Count == 1) {
                NetBlobRecord netBlobRecord = ((NetBlobRow)recordsListView.SelectedItem).netBlobRecord;

                bool wasUndetermined = netBlobRecord.messageErrorTypeOptional == MessageErrorType.UNDETERMINED;

                if (netBlobRecord.netBlob.payload != null) {
                    recordHexTextBox.Text = BitConverter.ToString(netBlobRecord.netBlob.payload);
                } else {
                    recordHexTextBox.Text = "";
                }

                Exception? messageException = netBlobRecord.messageException;
                if (messageException == null) {
                    try {
                        recordMessageTextBox.Text = Util.objectToString(netBlobRecord.message);
                    } catch (Exception ex) {
                        recordMessageTextBox.Text = ex.ToString();
                    }
                } else {
                    recordMessageTextBox.Text = $"{messageException}\n\nAt position: {netBlobRecord.parseFailurePos}";
                }

                if (wasUndetermined) {
                    recordsListView.Items.Refresh();
                }
            } else {
                recordHexTextBox.Text = "";
                recordMessageTextBox.Text = "";
            }
        });
    }
}
