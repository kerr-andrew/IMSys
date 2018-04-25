using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows;

namespace IMSys
{
    public class Item : INotifyPropertyChanged
    {
        static IMSysDBDataSetTableAdapters.InventoryTableAdapter inventoryAdapter = Application.Current.Properties["inventory"] as IMSysDBDataSetTableAdapters.InventoryTableAdapter;

        public string Name { get; set; }
        private decimal _price;
        public decimal Price { get { return _price; } set { if (_price != value) { _price = value; OnPropertyChanged("Value"); } } }

        private int _quantity;
        public int Quantity
        {
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged("Value");
                }
            }
            get
            {
                return _quantity;

            }
        }
        public string Unit { get; set; }
        public decimal Value { get { return Price * Quantity; } }

        public Item()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public Item(string itemName, decimal itemPrice, int itemQuantity, string itemUnit)
        {
            Name = itemName;
            Price = itemPrice;
            Quantity = itemQuantity;
            Unit = itemUnit;
                      
        }
        
        public static ObservableCollection<Item> GetItems()
        {
            var data = from row in inventoryAdapter.GetData().AsEnumerable()
                       select new Item(row.itemName, row.Price, row.Quantity, row.Unit);
            ObservableCollection<Item> items = new ObservableCollection<Item>(data);

            return items;
        }
        
        /*
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private string _name;
        public string Name
        {
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
            get
            {
                return _name;
            }
        }

        private int _quantity;
        public int Quantity
        {
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged("Quantity");
                }
            }
            get
            {
                return _quantity;
            }
        }

        private decimal _price;
        public decimal Price
        {
            set
            {
                if (_price != value)
                {
                    _price = value;
                    OnPropertyChanged("Price");
                }
            }
            get
            {
                return _price;
            }
        }*/


    }
}
