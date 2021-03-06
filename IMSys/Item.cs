﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Reflection;

namespace IMSys
{
    public class Item : INotifyPropertyChanged
    {
        static IMSysDBDataSetTableAdapters.InventoryTableAdapter inventoryAdapter = Application.Current.Properties["inventory"] as IMSysDBDataSetTableAdapters.InventoryTableAdapter;
        static IMSysDBDataSetTableAdapters.CategoriesTableAdapter categoryAdapter = Application.Current.Properties["Categories"] as IMSysDBDataSetTableAdapters.CategoriesTableAdapter;

        public int liId;

        public object this[string name]
        {
            get
            {
                return GetType().InvokeMember(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty, Type.DefaultBinder, this, null);
            }
            set
            {
                GetType().InvokeMember(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty, Type.DefaultBinder, this, new object[] { value });
            }
        }
        public string Name { get; set; }
        private decimal _price;
        public decimal Price {
            get { return _price; }
            set { if (_price != value) { _price = value; OnPropertyChanged("Value"); } } }        
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
        public decimal Value { get { return Price * Quantity; } set { } }
        private int _category;
        public int Category
        {
            get { return _category; }
            set
            {
                if (value != _category)
                {
                    _category = value;
                    OnPropertyChanged("Category");
                }
            }
        }
        public Item()
        {

        }

        public Item(string itemName)
        {
            Name = itemName;
        }

        public Item(int id, string itemName, decimal itemPrice, int itemQuantity, string itemUnit, int categoryId)
        {
            liId = id;
            Name = itemName;
            Price = itemPrice;
            Quantity = itemQuantity;
            Unit = itemUnit;
            Category = categoryId;

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private string getCategory(int categoryId)
        {       

            IMSys.Category category = IMSys.Category.GetCategoryName(categoryId);
            return category.Name;           
            
        }

        public static ObservableCollection<Item> GetItems()
        {
            var id = inventoryAdapter.GetData();
            var data = from row in id.AsEnumerable()
                       select new Item(row.liId, row.itemName, row.Price, row.Quantity, row.Unit, row.CategoryId);
            ObservableCollection<Item> items = new ObservableCollection<Item>(data);
            
            return items;
        }
        
        public static ObservableCollection<Item> GetItemNames()
        {
            var data = from row in inventoryAdapter.GetData().AsEnumerable()
                       select new Item(row.itemName);
                      
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
    public class InventoryViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public ObservableCollection<Category> Categories { get; set; }
        public InventoryViewModel()
        {
            Items = Item.GetItems();
            Categories = Category.GetCategories();
        }
        public Category SelectedItem { get; set; }
    }
}
