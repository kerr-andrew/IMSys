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
    /// Interaction logic for DeleteCategoriesPopup.xaml
    /// </summary>
    public partial class DeleteCategoriesPopup : Window
    {
        public DeleteCategoriesPopup()
        {
            InitializeComponent();
        }

        private void RemoveCategoryClick(object sender, RoutedEventArgs e)
        {
            Category.GetId(cbxReplacement.SelectedValue)
        }
    }
}
