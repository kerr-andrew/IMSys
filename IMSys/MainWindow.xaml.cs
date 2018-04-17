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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IMSysDBDataSetTableAdapters.InventoryTableAdapter inventoryAdapter = new IMSysDBDataSetTableAdapters.InventoryTableAdapter();
        public MainWindow()
        {
            InitializeComponent();

            Inventory.ItemsSource = inventoryAdapter.GetData();
        }

        private void Inventory_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            //SqlCommandBuilder scb = new SqlCommandBuilder();
            //scb.ConflictOption = System.Data.ConflictOption.OverwriteChanges;
            //inventoryAdapter.Adapter.UpdateCommand = scb.GetUpdateCommand();
            TextBox t = e.EditingElement as TextBox;  // Assumes columns are all TextBoxes
            DataGridColumn dgc = e.Column;

            Debug.WriteLine(t.Text .ToString());

            /*
            Console.WriteLine("Edited inventory: ");
            foreach (var item in Inventory.ItemsSource)
                Console.WriteLine((item as IMSysDBDataSet.InventoryRow).itemName);
            inventoryAdapter.Update(Inventory.ItemsSource as IMSysDBDataSet.InventoryDataTable);
            */
        }
    }
}
