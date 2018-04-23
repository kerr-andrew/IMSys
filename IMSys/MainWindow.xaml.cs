using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

            
            var data = from row in inventoryAdapter.GetData().AsEnumerable()
                       select new Item(row.itemName, row.Price, row.Quantity, row.Unit);
            ObservableCollection<Item> items = new ObservableCollection<Item>(data);
            Inventory.ItemsSource = inventoryAdapter.GetData();
        }

        public void Refresh()
        {
            
        }
    }
}
