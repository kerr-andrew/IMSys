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
        static MainWindow()
        {
            Application.Current.Properties["inventory"] = new IMSysDBDataSetTableAdapters.InventoryTableAdapter();
        }

        IMSysDBDataSetTableAdapters.InventoryTableAdapter inventoryAdapter = Application.Current.Properties["inventory"] as IMSysDBDataSetTableAdapters.InventoryTableAdapter;

        public MainWindow()
        {
           
            InitializeComponent();

            
            Inventory.ItemsSource = inventoryAdapter.GetData();
        }

        private void Inventory_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            TextBox t = e.EditingElement as TextBox;  // Assumes columns are all TextBoxes
            IMSysDBDataSet.InventoryRow inventoryRow = (e.Row.DataContext as System.Data.DataRowView).Row as IMSysDBDataSet.InventoryRow;
            DataGridColumn dataGridColumn = e.Column;                               
            inventoryAdapter.UpdateRow(inventoryRow.liId, t.Text, inventoryRow.Price, inventoryRow.Quantity, inventoryRow.Unit, inventoryRow.Value);
            Debug.WriteLine(inventoryRow.liId + "\n" + t.Text + "\n" + inventoryRow.Price + "\n" + inventoryRow.Quantity + "\n" + inventoryRow.Unit + "\n" + inventoryRow.Value);
        }
        
    }
}
