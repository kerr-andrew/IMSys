using System;
using System.Collections.Generic;
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

        public AddItem()
        {
            InitializeComponent();
        }

        private void AddItemClick(object sender, RoutedEventArgs e)
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
        }
    }
}
