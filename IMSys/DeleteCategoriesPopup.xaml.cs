using System;
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
    /// Interaction logic for DeleteCategoriesPopup.xaml
    /// </summary>
    public partial class DeleteCategoriesPopup : Window
    {
        static IMSysDBDataSetTableAdapters.CategoriesTableAdapter categoryAdapter = Application.Current.Properties["Categories"] as IMSysDBDataSetTableAdapters.CategoriesTableAdapter;

        public ObservableCollection<Category> UpdatedCategoryList { get; set; }
        public Category BeingRemoved { get; set; }
        public DeleteCategoriesPopup(Category removed, ObservableCollection<Category> updated)
        {
            BeingRemoved = removed;
            UpdatedCategoryList = updated;
            InitializeComponent();
        }

        private void RemoveCategoryClick(object sender, RoutedEventArgs e)
        {
            categoryAdapter.RemoveCategory(BeingRemoved.Id, (cbxReplacement.SelectedItem as Category).Id);
            InventoryViewModel.Model.ChangableCategories.Remove(BeingRemoved);
            (App.Current.Windows[0] as MainWindow).UpdateTable();
        }
    }
}
