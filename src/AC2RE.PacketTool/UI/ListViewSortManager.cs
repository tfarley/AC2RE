using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace AC2RE.PacketTool.UI;

internal class ListViewSortManager {

    private readonly ListView listView;
    private readonly string[] secondarySortPropertyNames;

    private GridViewColumnHeader? sortColumnHeader;
    private ListSortDirection sortDirection = ListSortDirection.Ascending;

    public ListViewSortManager(ListView listView, params string[] secondarySortPropertyNames) {
        this.listView = listView;
        this.secondarySortPropertyNames = secondarySortPropertyNames;
    }

    public void columnHeaderClicked(object sender, RoutedEventArgs e) {
        if (e.OriginalSource is GridViewColumnHeader headerClicked) {
            if (headerClicked.Role != GridViewColumnHeaderRole.Padding) {
                ListSortDirection direction;

                if (headerClicked != sortColumnHeader) {
                    direction = ListSortDirection.Ascending;
                } else {
                    if (sortDirection == ListSortDirection.Ascending) {
                        direction = ListSortDirection.Descending;
                    } else {
                        direction = ListSortDirection.Ascending;
                    }
                }

                Binding? columnBinding = headerClicked.Column.DisplayMemberBinding as Binding;
                string? propertyName = columnBinding?.Path.Path ?? headerClicked.Column.Header as string;

                listView.Items.SortDescriptions.Clear();
                listView.Items.SortDescriptions.Add(new(propertyName, direction));
                foreach (string secondarySortPropertyName in secondarySortPropertyNames) {
                    listView.Items.SortDescriptions.Add(new(secondarySortPropertyName, direction));
                }
                listView.Items.Refresh();

                sortColumnHeader = headerClicked;
                sortDirection = direction;
            }
        }
    }
}
