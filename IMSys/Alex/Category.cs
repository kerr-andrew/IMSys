using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace IMSys
{
    public class Category : INotifyPropertyChanged
    {
        static IMSysDBDataSetTableAdapters.CategoriesTableAdapter categoryAdapter = Application.Current.Properties["Categories"] as IMSysDBDataSetTableAdapters.CategoriesTableAdapter;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Id
        {
            get;
            set;
        }
        
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public void OnPropertyChanged(string prop)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            categoryAdapter.Update(Name, Id);
        }

        public Category(int id, string category)
        {
            Id = id;
            Name = category;
        }

        public static Category GetCategoryName(int categoryId)
        {
            string data = "";
            categoryAdapter.GetCategory(categoryId, ref data);

            Category name = new Category(categoryId, data);


            return name;
        }

        public static ObservableCollection<Category> GetCategories()
        {
            var rows = categoryAdapter.GetData();
            var data = from row in rows.AsEnumerable()
                       select new Category(row.liId, row.Name);
            ObservableCollection<Category> categories = new ObservableCollection<Category>(data);
            return categories;
        }

        public static implicit operator int(Category cat)
        {
            return cat.Id;
        }

        public static explicit operator Category(int i)
        {
            return GetCategoryName(i);
        }

        public static int GetId(Category cat)
        {
            return cat.Id;
        }

    }
    public class CategoryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Category.GetCategoryName((int)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as Category).Id;
        }
    }
}
