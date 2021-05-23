using AC2RE.UICommon;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace AC2RE.PacketTool.UI {

    public partial class ToolsDialog : Window {

        public ToolsDialog(MainWindow mainWindow) {
            InitializeComponent();

            foreach (string toolName in Tool.TOOLS.Keys) {
                toolComboBox.Items.Add(new ComboBoxItem { Content = toolName });
            }
        }

        private void toolsDialog_ContentRendered(object sender, EventArgs e) {
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

        private void runButton_Click(object sender, RoutedEventArgs e) {
            UIUtil.swallowException(() => {
                if (pathTextBox.Text == null || "".Equals(pathTextBox.Text)) {
                    return;
                }

                string? toolName = (string?)((ComboBoxItem?)toolComboBox.SelectedItem)?.Content;
                if (toolName != null && !"".Equals(toolName)) {
                    Tool.TOOLS[toolName].Invoke(pathTextBox.Text);
                }
            });
        }
    }
}
