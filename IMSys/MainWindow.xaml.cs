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
        public event Action Initializing;
        static MainWindow()
        {
            Application.Current.Properties["inventory"] = new IMSysDBDataSetTableAdapters.InventoryTableAdapter();
        }

        IMSysDBDataSetTableAdapters.InventoryTableAdapter inventoryAdapter = Application.Current.Properties["inventory"] as IMSysDBDataSetTableAdapters.InventoryTableAdapter;

        public MainWindow()
        {
           
            Initializing += InitializeComponent;
            Initializing += InitializeAndrew;
            Initializing += FillTable;

            Initializing();

        }

        public void FillTable()
        {
            Inventory.ItemsSource = inventoryAdapter.GetData();
        }
    }
}
