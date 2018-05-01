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
            System.Windows.Controls.TextBox t = e.EditingElement as System.Windows.Controls.TextBox;  // Assumes columns are all TextBoxes
            Item item = e.Row.DataContext as IMSys.Item;
            DataGridColumn dataGridColumn = e.Column;
            string a = "";
            if (e.Column is DataGridComboBoxColumn)
                a = "Category";
            else
                a = e.Column.Header.ToString();
            Debug.WriteLine(a);
            switch (a)
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
