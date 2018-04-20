namespace IMSys
{
    public static class InventoryManagementExtensionMethods
    {
        public static IMSysDBDataSet.InventoryRow InventoryRow(this System.Windows.Controls.DataGridCellInfo cell)
        {
            return (cell.Item as System.Data.DataRowView).Row as IMSysDBDataSet.InventoryRow;
        }
    }
}
