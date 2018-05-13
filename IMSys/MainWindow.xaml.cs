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
    public delegate void InitializingComponentsEvent();

    public partial class MainWindow : Window
    {
        static MainWindow()
        {
            Application.Current.Properties["inventory"] = new IMSysDBDataSetTableAdapters.InventoryTableAdapter();
            Application.Current.Properties["Categories"] = new IMSysDBDataSetTableAdapters.CategoriesTableAdapter();
        }

        IMSysDBDataSetTableAdapters.InventoryTableAdapter inventoryAdapter = Application.Current.Properties["inventory"] as IMSysDBDataSetTableAdapters.InventoryTableAdapter;

        public ObservableCollection<Category> Categories { get; } = Category.GetCategories();
        public DataGridComboBoxColumn col = null;

        public event InitializingComponentsEvent InitializingComponents;

        public MainWindow()
        {
            InitializingComponents += InitializeComponent;
            InitializingComponents += MainWindow_InitializingComponents;
            InitializingComponents += InitializeAndrew;
            InitializingComponents += InitializeAlex;

        }

        public void Initialize()
        {
            if (!IsInitialized)
                InitializingComponents();
        }

        private void MainWindow_InitializingComponents()
        {
            UpdateTable();
            Inventory.FastEdit();
        }

        public void UpdateTable()
        {
            (DataContext as InventoryViewModel).Items.Clear();
            foreach (var item in Item.GetItems())
                (DataContext as InventoryViewModel).Items.Add(item);
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs evt)
        {
            (sender as ComboBox).DropDownClosed += (s, e) => Inventory.CommitEdit();
        }

        private void searchFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            Inventory.CommitEdit();
        }
    }
}
