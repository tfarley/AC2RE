using AC2E.PacketTool.Reader;
using AC2E.Protocol.Message;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace AC2E.PacketTool.UI {

    public partial class MainWindow : Window {

        private readonly List<NetBlobRow> netBlobRows = new List<NetBlobRow>();

        private GridViewColumnHeader lastRecordsListBoxColumnHeaderClicked = null;
        private ListSortDirection lastRecordsListBoxColumnHeaderSortDirection = ListSortDirection.Ascending;

        public object INetServerEvent { get; private set; }

        public MainWindow() {
            InitializeComponent();
        }

        private void refreshItems() {
            object prevSelectedItem = recordsListBox.SelectedItem;

            recordsListBox.Items.Clear();

            string opcodeFilter = opcodeFilterTextBox.Text;
            string eventFilter = eventFilterTextBox.Text;
            string errorsFilter = errorsFilterComboBox.Text;

            bool reselect = false;
            foreach (NetBlobRow netBlobRow in netBlobRows) {
                if (opcodeFilter != null && !netBlobRow.opcodeName.Contains(opcodeFilter, StringComparison.InvariantCultureIgnoreCase)) {
                    continue;
                }

                if (eventFilter != null && !netBlobRow.eventName.Contains(eventFilter, StringComparison.InvariantCultureIgnoreCase)) {
                    continue;
                }

                if (errorsFilter == "Exclude" && netBlobRow.netBlobRecord.messageException != null) {
                    continue;
                }

                if (errorsFilter == "Only" && netBlobRow.netBlobRecord.messageException == null) {
                    continue;
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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Packet Captures (*.pcap;*.pcapng)|*.pcap;*.pcapng|All files (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == true) {
                lastRecordsListBoxColumnHeaderClicked = null;

                using (FileStream fileStream = File.OpenRead(openFileDialog.FileName)) {
                    NetBlobCollection netBlobCollection = PcapReader.read(fileStream);

                    netBlobRows.Clear();

                    int lineNum = 1;
                    foreach (NetBlobRecord netBlobRecord in netBlobCollection.records) {
                        NetBlobRow netBlobRow = new NetBlobRow(lineNum, netBlobRecord);
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
            GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
            ListSortDirection direction;

            if (headerClicked != null) {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding) {
                    if (headerClicked != lastRecordsListBoxColumnHeaderClicked) {
                        direction = ListSortDirection.Ascending;
                    } else {
                        if (lastRecordsListBoxColumnHeaderSortDirection == ListSortDirection.Ascending) {
                            direction = ListSortDirection.Descending;
                        } else {
                            direction = ListSortDirection.Ascending;
                        }
                    }

                    Binding columnBinding = headerClicked.Column.DisplayMemberBinding as Binding;
                    string sortBy = columnBinding?.Path.Path ?? headerClicked.Column.Header as string;

                    recordsListBox.Items.SortDescriptions.Clear();
                    recordsListBox.Items.SortDescriptions.Add(new SortDescription(sortBy, direction));
                    recordsListBox.Items.SortDescriptions.Add(new SortDescription("seq", direction));
                    recordsListBox.Items.Refresh();

                    lastRecordsListBoxColumnHeaderClicked = headerClicked;
                    lastRecordsListBoxColumnHeaderSortDirection = direction;
                }
            }
        }

        private void objectToString(StringBuilder stringBuilder, HashSet<object> visited, int indentLevel, object target) {
            if (target == null) {
                stringBuilder.Append("null");
                return;
            }

            Type targetType = target.GetType();

            if (targetType.IsPrimitive || target is Enum) {
                stringBuilder.Append(target.ToString());
                return;
            }

            IEnumerable enumerableValue = target as IEnumerable;
            if (enumerableValue != null && !(target is string)) {
                if (!visited.Add(target)) {
                    stringBuilder.Append($"Circular ref: {target}");
                    return;
                }

                stringBuilder.AppendLine("[");
                bool first = true;
                foreach (object val in enumerableValue) {
                    if (!first) {
                        stringBuilder.AppendLine(",");
                    }
                    stringBuilder.Append(' ', indentLevel + 2);
                    objectToString(stringBuilder, visited, indentLevel + 2, val);
                    first = false;
                }
                stringBuilder.AppendLine();
                stringBuilder.Append(' ', indentLevel);
                stringBuilder.Append(']');
                return;
            }

            foreach (MethodInfo methodInfo in targetType.GetMethods()) {
                if (methodInfo.Name == "ToString" && methodInfo.DeclaringType == targetType) {
                    stringBuilder.Append(target.ToString());
                    return;
                }
            }

            if (!visited.Add(target)) {
                stringBuilder.Append($"Circular ref: {target}");
                return;
            }

            FieldInfo[] fieldInfos = targetType.GetFields();
            stringBuilder.AppendLine("{");
            string fieldIndent = new string(' ', indentLevel + 2);
            foreach (FieldInfo fieldInfo in fieldInfos) {
                string fieldLine = $"{fieldIndent}{fieldInfo.Name} = ";
                stringBuilder.Append(fieldLine);
                objectToString(stringBuilder, visited, fieldLine.Length, fieldInfo.GetValue(target));
                stringBuilder.AppendLine();
            }
            stringBuilder.Append(' ', indentLevel);
            stringBuilder.Append('}');
        }

        private void recordsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (recordsListBox.SelectedItems.Count == 1) {
                NetBlobRecord netBlobRecord = ((NetBlobRow)recordsListBox.SelectedItem).netBlobRecord;

                bool wasUndetermined = netBlobRecord.messageErrorTypeOptional == NetBlobRecord.MessageErrorType.UNDETERMINED;

                if (netBlobRecord.netBlob.payload != null) {
                    recordHexTextBox.Text = BitConverter.ToString(netBlobRecord.netBlob.payload);
                } else {
                    recordHexTextBox.Text = "";
                }

                Exception messageException = netBlobRecord.messageException;
                if (messageException == null) {
                    try {
                        INetMessage message = netBlobRecord.message;

                        StringBuilder messageStringBuilder = new StringBuilder();
                        objectToString(messageStringBuilder, new HashSet<object>(), 0, message);

                        recordMessageTextBox.Text = messageStringBuilder.ToString();
                    } catch (Exception ex) {
                        recordMessageTextBox.Text = ex.ToString();
                    }
                } else {
                    recordMessageTextBox.Text = messageException.ToString();
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
