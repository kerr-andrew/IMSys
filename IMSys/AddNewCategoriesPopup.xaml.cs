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
    /// Interaction logic for ManageCategoriesPopup.xaml
    /// </summary>
    public partial class AddNewCategoriesPopup : Window
    {

        static IMSysDBDataSetTableAdapters.CategoriesTableAdapter categoryAdapter = Application.Current.Properties["Categories"] as IMSysDBDataSetTableAdapters.CategoriesTableAdapter;


        public AddNewCategoriesPopup()
        {
            InitializeComponent();
        }

        private void AddNewCategoryEvent(object sender, RoutedEventArgs e)
        {
            categoryAdapter.AddCategory(txtCategoryName.Text);
        }
        private void NewCategoryGotFocusEvent(object sender, RoutedEventArgs e)
        {
            txtCategoryName.Text = "";
        }

    }
}
