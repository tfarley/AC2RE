using AC2RE.UICommon;
using AC2RE.Utils;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace AC2RE.PacketTool.UI {

    public partial class SearchDialog : Window {

        private class SearchResult {

            public string fileName { get; init; }
            public int lineNum { get; init; }
            public string opcodeName { get; init; }
            public string eventName { get; init; }
            public Dictionary<string, object>? matchResultValues { get; init; }

            public SearchResult(string fileName, int lineNum, string opcodeName, string eventName, Dictionary<string, object>? matchResultValues) {
                this.lineNum = lineNum;
                this.fileName = fileName;
                this.opcodeName = opcodeName;
                this.eventName = eventName;
                this.matchResultValues = matchResultValues;
            }
        }

        private readonly MainWindow mainWindow;
        private readonly ListViewSortManager resultsListViewSortManager;
        private readonly ListViewExtraColumnManager resultsListViewExtraColumnManager;

        public SearchDialog(MainWindow mainWindow) {
            this.mainWindow = mainWindow;

            InitializeComponent();

            resultsListViewSortManager = new(resultsListView, "fileName", "lineNum");
            resultsListViewExtraColumnManager = new(resultsListView);

            foreach (MessageErrorType? messageErrorType in Enum.GetValues(typeof(MessageErrorType))) {
                if (messageErrorType != MessageErrorType.UNDETERMINED) {
                    errorsFilterComboBox.Items.Add(new ComboBoxItem { Content = messageErrorType });
                }
            }

            foreach (string customFilterName in CustomFilter.FILTERS.Keys) {
                customFilterComboBox.Items.Add(new ComboBoxItem { Content = customFilterName });
            }
        }

        private void searchDialog_ContentRendered(object sender, EventArgs e) {
            pathTextBox.Focus();
        }

        private void browseButton_Click(object sender, RoutedEventArgs e) {
            UIUtil.swallowException(() => {
                // There's no simple dialog to select a folder apparently... so this is really only useful for conveniently filling the path to some file and then manually changing the path if necessary
                OpenFileDialog openFileDialog = new();
                openFileDialog.Filter = "Packet Captures (*.pcap;*.pcapng)|*.pcap;*.pcapng|All files (*.*)|*.*";
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == true) {
                    pathTextBox.Text = Path.GetDirectoryName(openFileDialog.FileName);
                }
            });
        }

        private void searchButton_Click(object sender, RoutedEventArgs e) {
            UIUtil.swallowException(() => {
                if (pathTextBox.Text == null || "".Equals(pathTextBox.Text)) {
                    return;
                }

                resultsListView.Items.Clear();

                string opcodeFilter = opcodeFilterTextBox.Text;
                string eventFilter = eventFilterTextBox.Text;
                string? stringFilter = stringFilterHexCheckBox.IsChecked != true ? stringFilterTextBox.Text : null;
                byte[]? bytePatternFilter = stringFilterHexCheckBox.IsChecked == true ? Util.hexStringToBytes(stringFilterTextBox.Text) : null;
                object? errorsFilter = ((ComboBoxItem?)errorsFilterComboBox.SelectedItem)?.Content;
                bool showIncomplete = false;
                string? customFilterName = (string?)((ComboBoxItem?)customFilterComboBox.SelectedItem)?.Content;
                CustomFilter? customFilter = customFilterName != null && !"".Equals(customFilterName) ? CustomFilter.FILTERS[customFilterName].Invoke() : null;

                resultsListViewExtraColumnManager.setExtraColumns(customFilter?.resultColumns);

                PacketUtil.processAllPcaps(pathTextBox.Text, (pcapFileName, netBlobCollection) => {
                    int lineNum = 1;
                    foreach (NetBlobRecord netBlobRecord in netBlobCollection.records) {
                        NetBlobRow netBlobRow = new(lineNum, netBlobRecord);

                        if (netBlobRow.matches(opcodeFilter, eventFilter, errorsFilter, stringFilter, bytePatternFilter, showIncomplete, customFilter)) {
                            resultsListView.Items.Add(new SearchResult(pcapFileName, lineNum, netBlobRow.opcodeName, netBlobRow.eventName, netBlobRow.matchResultValues));
                        }

                        lineNum++;
                    }
                });
            });
        }

        private void resultsListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            UIUtil.swallowException(() => {
                if (resultsListView.SelectedItems.Count == 1) {
                    SearchResult searchResult = (SearchResult)resultsListView.SelectedItem;
                    mainWindow.openFile(searchResult.fileName);
                    mainWindow.goToLine(searchResult.lineNum, true);
                }
            });
        }

        private void resultsListViewColumnHeader_Click(object sender, RoutedEventArgs e) {
            UIUtil.swallowException(() => {
                resultsListViewSortManager.columnHeaderClicked(sender, e);
            });
        }
    }
}
