using System.Collections.Generic;
using System.Windows.Controls;

namespace AC2RE.PacketTool.UI;

internal class ListViewExtraColumnManager {

    private readonly ListView listView;

    private readonly List<GridViewColumn> extraColumns = new();

    public ListViewExtraColumnManager(ListView listView, params string[] secondarySortPropertyNames) {
        this.listView = listView;
    }

    public void setExtraColumns(List<GridViewColumn>? columns) {
        GridViewColumnCollection viewColumns = ((GridView)listView.View).Columns;
        foreach (GridViewColumn matchResultColumn in extraColumns) {
            viewColumns.Remove(matchResultColumn);
        }

        extraColumns.Clear();

        if (columns != null) {
            extraColumns.AddRange(columns);
            foreach (GridViewColumn column in columns) {
                viewColumns.Add(column);
            }
        }
    }
}
