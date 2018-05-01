using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for DeleteItem.xaml
    /// </summary>
    public partial class DeleteItem : Window
    {

        IMSysDBDataSetTableAdapters.InventoryTableAdapter inventoryAdapter = Application.Current.Properties["inventory"] as IMSysDBDataSetTableAdapters.InventoryTableAdapter;


        public DeleteItem()
        {
            InitializeComponent();
            lbxItems.ItemsSource = Item.GetItemNames();
            ICollectionView view = CollectionViewSource.GetDefaultView(lbxItems.ItemsSource);
            view.Filter = SearchFilter;
        }

        

        public void AddItemToDeleteList(object sender, RoutedEventArgs e)
        {
            if (!lbxDeletedItems.Items.Contains(lbxItems.SelectedItem))
            {
                lbxDeletedItems.Items.Add(lbxItems.SelectedItem);
                lblwarning.Visibility = Visibility.Hidden;
            }
            else
            {
                lblwarning.Content = "That Item is already in the deleted items list!";
                lblwarning.Visibility = Visibility.Visible;
            }
        }

        public void RemoveItemFromDeleteList(object sender, RoutedEventArgs e)
        {
            if (lbxDeletedItems.SelectedIndex != -1)
            {
                lbxDeletedItems.Items.Remove(lbxDeletedItems.SelectedItem);
                lblwarning.Visibility = Visibility.Hidden;
            }
            else
            {
                lblwarning.Content = "You haven't selected an item in the deleted items list to remove!";
                lblwarning.Visibility = Visibility.Visible;
            }
        }

        public void DeleteItems(object sender, RoutedEventArgs e)
        {
            foreach(Item item in lbxDeletedItems.Items)
            {
                inventoryAdapter.DeleteItems(item.Name);
            }
        }

        private bool SearchFilter(object item)
        {
            if (String.IsNullOrEmpty(txtSearch.Text))
                return true;
            else
                return ((item as Item).Name.IndexOf(txtSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void TxtSearchItems(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lbxItems.ItemsSource).Refresh();
        }
    }
}
