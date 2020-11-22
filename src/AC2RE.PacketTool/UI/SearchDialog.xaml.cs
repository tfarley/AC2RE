using System;
using System.Windows;

namespace AC2RE.PacketTool.UI {

    public partial class SearchDialog : Window {

        public string path => pathTextBox.Text;

        public SearchDialog() {
            InitializeComponent();
        }

        private void searchDialog_ContentRendered(object sender, EventArgs e) {
            pathTextBox.Focus();
        }

        private void searchButton_Click(object sender, RoutedEventArgs e) {

        }
    }
}
