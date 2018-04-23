using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
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
using System.Windows.Shapes;

namespace IMSys
{
    /// <summary>
    /// Interaction logic for AddItem.xaml
    /// </summary>
    public partial class AddItem : Window
    {

        IMSysDBDataSetTableAdapters.InventoryTableAdapter inventoryAdapter = Application.Current.Properties["inventory"] as IMSysDBDataSetTableAdapters.InventoryTableAdapter;
        public ObservableCollection<Item> ItemsCollection { get; set; } = new ObservableCollection<Item>()
        ;

        public AddItem()
        {
            InitializeComponent();
            addItemGrid.SetBinding(DataGrid.ItemsSourceProperty, new Binding()
            {
                Source = this,
                Path = new PropertyPath("ItemsCollection"),
                Mode = BindingMode.TwoWay
            });


        }

        private void Grid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Value")
            {
                // e.Cancel = true;   // For not to include 
                e.Column.IsReadOnly = true; // Makes the column as read only
            }

        }

        private void addItems(object sender, RoutedEventArgs e)
        {
            int count = 0;
            foreach (Item item in ItemsCollection) {
                inventoryAdapter.AddRow(
                    ItemsCollection.ElementAt<Item>(count).Name.ToString(),
                    ItemsCollection.ElementAt<Item>(count).Price,
                    ItemsCollection.ElementAt<Item>(count).Quantity,
                    ItemsCollection.ElementAt<Item>(count).Unit.ToString(),
                    ItemsCollection.ElementAt<Item>(count).Value
                );
            count++;
                inventoryAdapter.Fill((Application.Current.Windows[0] as MainWindow).Inventory.ItemsSource as IMSysDBDataSet.InventoryDataTable);
            }
        }

        /*private void AddItemClick(object sender, RoutedEventArgs e)
        {
            
            string name = txtItemName.Text;
            decimal price;
            bool result1 = Decimal.TryParse(txtPrice.Text, out price);
            int quantity;
            bool result2 = Int32.TryParse(txtQuantity.Text, out quantity);
            string unit = txtUnit.Text;
            decimal? value = null;

            if (result1 && result2) {
                value = price * quantity;
            }
            if(value != null)
                inventoryAdapter.AddRow(name, price, quantity, unit, value.Value);
        }*/
    }
}
