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
using System.Windows.Forms;
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
            System.Windows.Controls.TextBox t = e.EditingElement as System.Windows.Controls.TextBox;  // Assumes columns are all TextBoxes
            Item item = e.Row.DataContext as IMSys.Item;
            //IMSysDBDataSet.InventoryRow inventoryRow = (e.Row.DataContext as System.Data.DataRowView).Row as IMSysDBDataSet.InventoryRow;
            DataGridColumn dataGridColumn = e.Column;
            string a = "";
            if (e.Column is DataGridComboBoxColumn)
                a = "Category";
            else
                a = e.Column.Header.ToString();
            Debug.WriteLine(a);
            switch (a)
            {
                case "Name":
                    item.Name = t.Text;
                    break;
                case "Price":
                    decimal tempd;
                    if (Decimal.TryParse(t.Text, out tempd))
                        item.Price = tempd;
                    break;
                case "Quantity":
                    int tempi;
                    if (int.TryParse(t.Text, out tempi))
                        item.Quantity = tempi;
                    break;
                case "Unit":
                    item.Unit = t.Text;
                    break;
                case "Category":
                    item.Category = (int)(e.EditingElement as System.Windows.Controls.ComboBox).SelectedValue;
                    break;
                default:
                    break;
            }
            inventoryAdapter.UpdateRow(item.liId, item.Name, item.Price, item.Quantity, item.Unit, item.Category);
        }

        private void AddItemClick(object sender, RoutedEventArgs e)
        {
            
            AddItem addItemWindow = new AddItem();
            addItemWindow.Owner = this;
            addItemWindow.Show();
        }

        private void DeleteItemClick(object sender, RoutedEventArgs e)
        {
            DeleteItem deleteItemWindow = new DeleteItem();
            deleteItemWindow.Owner = this;
            deleteItemWindow.Show();
        }

        public void FIllItemSource()
        {
            Inventory.ItemsSource = Item.GetItems();
            Inventory.FastEdit();
        }

    }
}
