using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IMSys
{
    public partial class MainWindow : Window
    {

        private void Inventory_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            TextBox t = e.EditingElement as TextBox;  // Assumes columns are all TextBoxes
            IMSysDBDataSet.InventoryRow inventoryRow = (e.Row.DataContext as System.Data.DataRowView).Row as IMSysDBDataSet.InventoryRow;
            DataGridColumn dataGridColumn = e.Column;
            var a = e.Column.Header.ToString();
            Debug.WriteLine(a);
            switch (a) {
                case "itemItem":
                    inventoryAdapter.UpdateRow(inventoryRow.liId, t.Text,
                    inventoryRow.Price, inventoryRow.Quantity, inventoryRow.Unit, inventoryRow.Value);
                    break;
                case "Price":
                    inventoryAdapter.UpdateRow(inventoryRow.liId, inventoryRow.itemName, Decimal.Parse(t.Text),
                    inventoryRow.Quantity, inventoryRow.Unit, inventoryRow.Value);
                    break;
                case "Quantity":
                    inventoryAdapter.UpdateRow(inventoryRow.liId, inventoryRow.itemName, inventoryRow.Price,
                    Int32.Parse(t.Text), inventoryRow.Unit, inventoryRow.Value);
                    break;
                case "Unit":
                    inventoryAdapter.UpdateRow(inventoryRow.liId, inventoryRow.itemName, inventoryRow.Price,
                    inventoryRow.Quantity, t.Text, inventoryRow.Value);
                    break;
                case "Value":
                    inventoryAdapter.UpdateRow(inventoryRow.liId, inventoryRow.itemName, inventoryRow.Price,
                    inventoryRow.Quantity, inventoryRow.Unit, Decimal.Parse(t.Text));
                    break;
                default:
                    break;
            }

        }

        private void addItem_Click(object sender, RoutedEventArgs e)
        {
            AddItem addItemWindow = new AddItem();
            addItemWindow.Show();
        }

    }
}
