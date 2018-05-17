using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace IMSys
{
    /// <summary>
    /// Interaction logic for ManageCategories.xaml
    /// </summary>
    public partial class ManageCategories : Window
    {

        static IMSysDBDataSetTableAdapters.CategoriesTableAdapter categoryAdapter = Application.Current.Properties["Categories"] as IMSysDBDataSetTableAdapters.CategoriesTableAdapter;

        public ManageCategories()
        {
            InitializeComponent();
        }

        private void CategorySelectedEvent(object sender, RoutedEventArgs e)
        {
            btnDelete.IsEnabled = true;
            btnRename.IsEnabled = true;
            lblRename.Visibility = Visibility.Hidden;
            txtRename.Visibility = Visibility.Hidden;
            btnRenameCategory.Visibility = Visibility.Hidden;
            txtRename.Text = "Name";
            
        }

        private void AddNewCategoryEvent(object sender, RoutedEventArgs e)
        {

            AddNewCategoriesPopup addNewCategoryWindow = new AddNewCategoriesPopup();
            addNewCategoryWindow.Owner = this;
            addNewCategoryWindow.Show();
        }

        private void DeleteCategoryEvent(object sender, RoutedEventArgs e)
        {
            DeleteCategoriesPopup deleteCategoriesWindow = new DeleteCategoriesPopup();
            deleteCategoriesWindow.Owner = this;
            deleteCategoriesWindow.Show();
        }

        private void ShowRenameCategoryEvent(object sender, RoutedEventArgs e)
        {
            lblRename.Visibility = Visibility.Visible;
            txtRename.Visibility = Visibility.Visible;
            btnRenameCategory.Visibility = Visibility.Visible;
        }

        private void RenameGotFocusEvent(object sender, RoutedEventArgs e)
        {
            txtRename.Text = "";
        }

        private void RenameCategoryEvent(object sender, RoutedEventArgs e)
        {
            categoryAdapter.RenameCategory(cbxSelectedCategory.Text, txtRename.Text);
        }
    }
}
