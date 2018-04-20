using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace IMSys
{
    /// <summary>
    /// Interaction logic for QuantityChangeControl.xaml
    /// </summary>
    public partial class QuantityChangeControl : UserControl
    {
        
        bool changeType = false;
        public Notifiable<int> Quantity { get; set; } = new Notifiable<int>();
        public IMSysDBDataSet.InventoryRow ItemRow;
        IMSysDBDataSetTableAdapters.InventoryTableAdapter inventory = Application.Current.Properties["inventory"] as IMSysDBDataSetTableAdapters.InventoryTableAdapter;

        public QuantityChangeControl(IMSysDBDataSet.InventoryRow row)
        {
            InitializeComponent();
            ItemRow = row;
            Quantity.Value = ItemRow.Stock;

            inputBox.SetBinding(TextBox.TextProperty, new Binding()
            {
                Source = this,
                Path = new PropertyPath("Quantity.Value"),
                Mode = BindingMode.TwoWay,
            });
            Loaded += QuantityChangeControl_Loaded;
        }

        private void QuantityChangeControl_Loaded(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.PropertyChanged += SettingChanged;

            Focus();
            ModeChange(Properties.Settings.Default.QuantityChangeType, true);
        }

        void SettingChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "QuantityChangeType")
            {
                ModeChange(Properties.Settings.Default.QuantityChangeType);
            }
        }
        void ModeChange()
        {
            ModeChange(!changeType);
        }
        void ModeChange(bool change, bool force = false)
        {
            if (!force)
            {
                if (changeType == change)
                    return;
                changeType = change;
            } else
            {
                changeType = change;
            }
            if (changeType)
            {
                //true == Absolute
                Quantity.Value = ItemRow.Stock + Quantity.Value;
                absEllipse.Visibility = Visibility.Hidden;
                inputAbsBtn.IsEnabled = false;
                chgEllipse.Visibility = Visibility.Visible;
                inputChgBtn.IsEnabled = true;
                
            }
            else
            {
                //false == plus minus
                Quantity.Value = Quantity.Value - ItemRow.Stock;
                absEllipse.Visibility = Visibility.Visible;
                inputAbsBtn.IsEnabled = true;
                chgEllipse.Visibility = Visibility.Hidden;
                inputChgBtn.IsEnabled = false;
            }

            Keyboard.Focus(inputBox);

        }

        private void inputAbsBtn_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.QuantityChangeType = true;
        }

        private void inputChgBtn_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.QuantityChangeType = false;
        }

        private void inputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var value = ItemRow.Stock;
                if (changeType)
                    value = int.Parse(inputBox.Text);
                else
                    value += int.Parse(inputBox.Text);

                ItemRow.Stock = value;

                var parent = Application.Current.Windows[0] as MainWindow;
                var builder = new System.Data.SqlClient.SqlCommandBuilder(inventory.Adapter);
                inventory.Adapter.UpdateCommand = builder.GetUpdateCommand();
                inventory.Update(ItemRow);

                parent.DropClickMenuIfActive();
            }
        }
    }
}
