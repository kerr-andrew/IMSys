using System.Windows.Controls;
using System.Windows.Data;

namespace IMSys
{
    public static class InventoryManagementExtensionMethods
    {
        public static Item InventoryRow(this System.Windows.Controls.DataGridCellInfo cell)
        {
            if (cell.Item == CollectionView.NewItemPlaceholder) return null;
            return cell.Item as Item;
        }
        public static bool IsActiveChangeControl(this DataGridCellInfo? dgci, Controls.IChangeControl qcc)
        {
            return dgci.HasValue &&
                dgci.Value.Item != null &&
                dgci.Value.InventoryRow() != null &&
                (qcc != null && qcc.Item.liId == dgci.Value.InventoryRow().liId) &&
                (string)dgci.Value.Column.Header == qcc.Column;
        }
    }
}
