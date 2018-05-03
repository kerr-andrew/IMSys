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
            Inventory.Loaded += Inventory_Loaded;

            UpdateTable();
            Inventory.FastEdit();
        }

        private void Inventory_Loaded(object sender, RoutedEventArgs e)
        {
           /* col = new DataGridComboBoxColumn
            {
                Header = "Category",
                ItemsSource = Category.GetCategories(),
                
               
            };
            col.DisplayMemberPath = "Name";
            col.TextBinding = new Binding("Name") { Mode = BindingMode.TwoWay };
            
            Inventory.Columns[Inventory.Columns.Count - 1] = col;
            Inventory.FastEdit();*/
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = Inventory.CurrentItem as Item;
            var box = sender as ComboBox;
            var cat = box.SelectedItem as Category;
        }

        public void UpdateTable()
        {
            (DataContext as InventoryViewModel).Items.Clear();
            foreach (var item in Item.GetItems())
                (DataContext as InventoryViewModel).Items.Add(item);

        }
    }
}
