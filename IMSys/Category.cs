using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            var data = categoryAdapter.GetCategory(categoryId);

            Category name = new Category(categoryId, data as string);


            return name;
        }

        public static ObservableCollection<Category> GetCategories()
        {
            var data = from row in categoryAdapter.GetData().AsEnumerable()
                       select new Category(row.liId, row.CategoryName);
            ObservableCollection<Category> categories = new ObservableCollection<Category>(data);
            return categories;
        }
    }
}
