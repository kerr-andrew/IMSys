using System.Windows.Controls;
using System.Windows.Data;

namespace IMSys
{
    public static class InventoryManagementExtensionMethods
    {
        public static IMSysDBDataSet.InventoryRow InventoryRow(this System.Windows.Controls.DataGridCellInfo cell)
        {
            if (cell.Item == CollectionView.NewItemPlaceholder) return null;
            return (cell.Item as System.Data.DataRowView).Row as IMSysDBDataSet.InventoryRow;
        }
        public static bool IsActiveChangeControl(this DataGridCellInfo? dgci, Controls.IChangeControl qcc)
        {
            return dgci.HasValue &&
                dgci.Value.Item != null &&
                dgci.Value.InventoryRow() != null &&
                (qcc != null && qcc.ItemRow.liId == dgci.Value.InventoryRow().liId) &&
                (string)dgci.Value.Column.Header == qcc.Column;
        }
    }
}
