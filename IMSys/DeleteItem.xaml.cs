using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        ObservableCollection<Item> itemNames = Item.GetItemNames();
        ArrayList removableItems = new ArrayList();


        public DeleteItem()
        {
            InitializeComponent();
            lbxItems.ItemsSource = Item.GetItemNames();

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
                lblwarning.Content = "You haven't selected an item in the delted items list to remove!";
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

        private void TxtSearchItems(object sender, TextChangedEventArgs e)
        {
           /*ArrayList itemsToRemove = new ArrayList();
            string searchString = txtSearch.Text;
            int count = 0;
            foreach (Item itemName in itemNames)
            {
                removableItems.Add(itemName.Name);
            }
            
            foreach(string str in removableItems)
            {
                string str1 = removableItems[count].ToString();

                if (str1.Contains(searchString))
                {
                    itemsToRemove.Add(str1);
                }
                count++;
            }

            foreach (string str in itemsToRemove)
            {
                lbxItems.Items.Remove(str);
            }





            if(removableItems.Contains(searchString) == true) 
            {
                removableItems
                int i = removableItems.IndexOf(searchString);
                itemsToRemove.Add(removableItems[i]);
                removableItems.Remove;
            } 
            */


        }
    }
}
