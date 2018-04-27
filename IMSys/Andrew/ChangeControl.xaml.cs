using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using IChangeControl;
namespace IMSys
{
    /// <summary>
    /// Interaction logic for ChangeControl.xaml
    /// </summary>

    public partial class ChangeControl<T, CE> : UserControl
        where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T> 
        where CE : IInventoryColumnEnum
    {

        public static string ColumnName { get; } = (string)typeof(CE).GetProperty("Name", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).GetValue(null);

        static ChangeControl()
        {

        }

        public DualValue<T> ColumnValue { get; set; } = new DualValue<T>();
        public override IChangeControl.IMSysDBDataSet.InventoryRow ItemRow { get; set; }
        IMSysDBDataSetTableAdapters.InventoryTableAdapter inventory = Application.Current.Properties["inventory"] as IMSysDBDataSetTableAdapters.InventoryTableAdapter;

        public ChangeControl(IChangeControl.IMSysDBDataSet.InventoryRow row) 
        {
            InitializeComponent();
            ItemRow = row;

            inputAbsBox.SetBinding(TextBox.TextProperty, new Binding()
            {
                Source = this,
                Path = new PropertyPath("ColumnValue.Absolute"),
                Mode = BindingMode.TwoWay,
            });
            inputChgBox.SetBinding(TextBox.TextProperty, new Binding()
            {
                Source = this,
                Path = new PropertyPath("ColumnValue.Change"),
                Mode = BindingMode.TwoWay
            });

            //can't pass enum or string as type parameter, could use string as instantiation parameter but meh, we'll sell can be easily changed
            //I just like the look up ChangeControl<decimal, IInventoryColumnEnum.Quantity>
            ColumnValue.Absolute = (dynamic)ItemRow[ColumnName];

            Loaded += QuantityChangeControl_Loaded;
        }

        private void QuantityChangeControl_Loaded(object sender, RoutedEventArgs e)
        { 

            Focus();
            Keyboard.Focus(inputAbsBox);
        }

        private void inputAbsBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var value = ItemRow[ColumnName];

                ColumnValue.Absolute = (T)Convert.ChangeType(value, typeof(T));

                ItemRow[ColumnName] = value;

                var parent = Application.Current.Windows[0] as MainWindow;
                var builder = new System.Data.SqlClient.SqlCommandBuilder(inventory.Adapter);
                inventory.Adapter.UpdateCommand = builder.GetUpdateCommand();
                inventory.Update(ItemRow);

                parent.DropClickMenuIfActive();
            }
        }
        private void inputChgBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var value = ItemRow[ColumnName];
                ColumnValue.Change = (T)Convert.ChangeType(value, typeof(T));
            }
        }
    }
}
