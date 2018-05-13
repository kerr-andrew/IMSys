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
                    ItemsCollection.ElementAt(count).Name.ToString(),
                    ItemsCollection.ElementAt(count).Price,
                    ItemsCollection.ElementAt(count).Quantity,
                    ItemsCollection.ElementAt(count).Unit.ToString(),
                    ItemsCollection.ElementAt(count).Category
                );
                count++;
            }

            foreach (Item item in ItemsCollection)
                inventoryAdapter.AddRow(item.Name, item.Price, item.Quantity, item.Unit, item.Category);

            (Owner as MainWindow).UpdateTable();
        }
    }
}
