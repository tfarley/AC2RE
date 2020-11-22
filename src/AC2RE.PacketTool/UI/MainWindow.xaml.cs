using AC2RE.Utils;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace AC2RE.PacketTool.UI {

    public partial class MainWindow : Window {

        private readonly string originalTitle;

        private readonly List<NetBlobRow> netBlobRows = new();

        private GridViewColumnHeader? lastRecordsListBoxColumnHeaderClicked;
        private ListSortDirection lastRecordsListBoxColumnHeaderSortDirection = ListSortDirection.Ascending;

        public MainWindow() {
            InitializeComponent();

            originalTitle = Title;

            foreach (MessageErrorType? messageErrorType in Enum.GetValues(typeof(MessageErrorType))) {
                if (messageErrorType != MessageErrorType.UNDETERMINED) {
                    errorsFilterComboBox.Items.Add(new ComboBoxItem { Content = messageErrorType });
                }
            }
        }

        private void refreshItems() {
            object prevSelectedItem = recordsListBox.SelectedItem;

            recordsListBox.Items.Clear();

            string opcodeFilter = opcodeFilterTextBox.Text;
            string eventFilter = eventFilterTextBox.Text;
            ComboBoxItem errorsFilter = (ComboBoxItem)errorsFilterComboBox.SelectedItem;
            bool showIncomplete = showIncompleteCheckBox.IsChecked ?? false;

            bool reselect = false;
            foreach (NetBlobRow netBlobRow in netBlobRows) {
                if (opcodeFilter != null && !netBlobRow.opcodeName.Contains(opcodeFilter, StringComparison.OrdinalIgnoreCase)) {
                    continue;
                }

                if (eventFilter != null && !netBlobRow.eventName.Contains(eventFilter, StringComparison.OrdinalIgnoreCase)) {
                    continue;
                }

                if (!showIncomplete && netBlobRow.netBlobRecord.netBlob.payload == null) {
                    continue;
                }

                if (errorsFilter != null && errorsFilter.Content != null && !"".Equals(errorsFilter.Content)) {
                    if ("All".Equals(errorsFilter.Content)) {
                        if (netBlobRow.netBlobRecord.messageException == null) {
                            continue;
                        }
                    } else if (!netBlobRow.netBlobRecord.messageErrorType.Equals(errorsFilter.Content)) {
                        continue;
                    }
                }

                if (netBlobRow == prevSelectedItem) {
                    reselect = true;
                }

                recordsListBox.Items.Add(netBlobRow);
            }

            if (reselect) {
                recordsListBox.SelectedItem = prevSelectedItem;
                recordsListBox.ScrollIntoView(recordsListBox.SelectedItem);
            }
        }

        private void openMenuItem_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "Packet Captures (*.pcap;*.pcapng)|*.pcap;*.pcapng|All files (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == true) {
                Title = $"{originalTitle} - {openFileDialog.SafeFileName}";

                lastRecordsListBoxColumnHeaderClicked = null;

                using (FileStream fileStream = File.OpenRead(openFileDialog.FileName)) {
                    NetBlobCollection netBlobCollection = PcapReader.read(fileStream);

                    netBlobRows.Clear();

                    int lineNum = 1;
                    foreach (NetBlobRecord netBlobRecord in netBlobCollection.records) {
                        NetBlobRow netBlobRow = new(lineNum, netBlobRecord);
                        netBlobRows.Add(netBlobRow);
                        recordsListBox.Items.Add(netBlobRow);
                        lineNum++;
                    }

                    refreshItems();
                }
            }
        }

        private void searchMenuItem_Click(object sender, RoutedEventArgs e) {
            new SearchDialog().Show();
        }

        private void exportJsonMenuItem_Click(object sender, RoutedEventArgs e) {

        }

        private void goToSelectedButton_Click(object sender, RoutedEventArgs e) {
            if (recordsListBox.SelectedItem != null) {
                recordsListBox.ScrollIntoView(recordsListBox.SelectedItem);
            }
        }

        private void goToLineButton_Click(object sender, RoutedEventArgs e) {
            if (int.TryParse(goToLineTextBox.Text, out int targetLine)) {
                targetLine = Math.Clamp(targetLine, 1, recordsListBox.Items.Count) - 1;
                recordsListBox.ScrollIntoView(recordsListBox.Items[targetLine]);
            }
        }

        private void applyFiltersButton_Click(object sender, RoutedEventArgs e) {
            refreshItems();
        }

        private void recordsListBoxColumnHeader_Click(object sender, RoutedEventArgs e) {
            if (e.OriginalSource is GridViewColumnHeader headerClicked) {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding) {
                    ListSortDirection direction;

                    if (headerClicked != lastRecordsListBoxColumnHeaderClicked) {
                        direction = ListSortDirection.Ascending;
                    } else {
                        if (lastRecordsListBoxColumnHeaderSortDirection == ListSortDirection.Ascending) {
                            direction = ListSortDirection.Descending;
                        } else {
                            direction = ListSortDirection.Ascending;
                        }
                    }

                    Binding? columnBinding = headerClicked.Column.DisplayMemberBinding as Binding;
                    string? sortBy = columnBinding?.Path.Path ?? headerClicked.Column.Header as string;

                    recordsListBox.Items.SortDescriptions.Clear();
                    recordsListBox.Items.SortDescriptions.Add(new(sortBy, direction));
                    recordsListBox.Items.SortDescriptions.Add(new("seq", direction));
                    recordsListBox.Items.Refresh();

                    lastRecordsListBoxColumnHeaderClicked = headerClicked;
                    lastRecordsListBoxColumnHeaderSortDirection = direction;
                }
            }
        }

        private void recordsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (recordsListBox.SelectedItems.Count == 1) {
                NetBlobRecord netBlobRecord = ((NetBlobRow)recordsListBox.SelectedItem).netBlobRecord;

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
                    recordsListBox.Items.Refresh();
                }
            } else {
                recordHexTextBox.Text = "";
                recordMessageTextBox.Text = "";
            }
        }
    }
}
