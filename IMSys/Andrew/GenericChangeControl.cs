using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace IMSys.Controls
{
    /// <summary>
    /// Interaction logic for ChangeControl.xaml
    /// </summary>
    public abstract class IChangeControl : UserControl
    {
        public abstract IMSysDBDataSet.InventoryRow ItemRow { get; set; }
        public abstract string Column { get; }
    }
    public class ChangeControl<T, CE> : IChangeControl
        where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
        where CE : IInventoryColumnEnum
    {
        public DualValue<T> ColumnValue { get; set; }

        public override IMSysDBDataSet.InventoryRow ItemRow { get; set; }

        IMSysDBDataSetTableAdapters.InventoryTableAdapter inventory = Application.Current.Properties["inventory"] as IMSysDBDataSetTableAdapters.InventoryTableAdapter;

        public static string ColumnName { get; } = (string)typeof(CE).GetProperty("Name", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).GetValue(null);
        public override string Column => ColumnName;

        //interface items
        internal TextBox inputAbsBox;
        internal TextBox inputChgBox;

        internal Button inputAbsBtn;
        internal Button inputChgBtn;

        internal Border border;

        internal Grid grid;

        private bool _contentLoaded = false;

        public void InitializeComponent()
        {
            if (_contentLoaded)
                return;

            Width = 124;
            Height = 54;
            Background = Brushes.Transparent;

            border = new Border()
            {
                CornerRadius = new CornerRadius(2.5, 2.5, 2.5, 2.5),
                Background = Brushes.Gainsboro,
                BorderThickness = new Thickness(1, 1, 1, 1),
                BorderBrush = Brushes.Gray
            };

            grid = new Grid();

            inputAbsBox = new TextBox()
            {
                Margin = new Thickness(2, 2, 0, 0),
                Width = 100,
                FontSize = 16,
                IsEnabled = true,
                Focusable = true,
                TextWrapping = TextWrapping.NoWrap,
                AcceptsReturn = false,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            inputAbsBox.KeyDown += inputBoxes_KeyDown;

            inputChgBox = new TextBox()
            {
                Margin = new Thickness(2, 0, 0, 2),
                Width = 100,
                FontSize = 16,
                IsEnabled = true,
                Focusable = true,
                TextWrapping = TextWrapping.NoWrap,
                AcceptsReturn = false,
                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Left,
            };
            var abs_bind = new Binding()
            {
                Source = this,
                Path = new PropertyPath("ColumnValue.Absolute"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            var chg_bind = new Binding()
            {
                Source = this,
                Path = new PropertyPath("ColumnValue.Change"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            if (new Type[] { typeof(decimal), typeof(float), typeof(double) }.Contains(typeof(T)))
            {
                chg_bind.StringFormat = "N2";
                abs_bind.StringFormat = "N2";
            }
            inputAbsBox.SetBinding(TextBox.TextProperty, abs_bind);
            inputChgBox.SetBinding(TextBox.TextProperty, chg_bind);

            inputChgBox.KeyDown += inputBoxes_KeyDown;

            inputAbsBtn = new Button()
            {
                Margin = new Thickness(0, 4, 0, 0),
                Height = 20,
                Width = 20,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Top,
                Background = Brushes.Transparent,
                BorderThickness = new Thickness(0),
                IsEnabled = false,
                Style = Application.Current.TryFindResource(ToolBar.ButtonStyleKey) as Style,
                Content = new Image()
                {
                    Source = new BitmapImage(new Uri("pack://application:,,,/Resources/equal.png"))
                }
            };

            inputChgBtn = new Button()
            {
                Margin = new Thickness(0, 0, 0, 3),
                Height = 20,
                Width = 20,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                Background = Brushes.Transparent,
                BorderThickness = new Thickness(0),
                IsEnabled = false,
                Style = Application.Current.TryFindResource(ToolBar.ButtonStyleKey) as Style,
                Content = new Image()
                {
                    Source = new BitmapImage(new Uri("pack://application:,,,/Resources/plusmin.png"))
                }
            };
            grid.Children.Add(inputAbsBox);
            grid.Children.Add(inputChgBox);
            grid.Children.Add(inputAbsBtn);
            grid.Children.Add(inputChgBtn);

            border.Child = grid;
            
            Content = border;

            FocusManager.SetIsFocusScope(grid, true);
            FocusManager.SetFocusedElement(this, inputAbsBox);

            _contentLoaded = true;
        }


        public ChangeControl(IMSysDBDataSet.InventoryRow row)
        {
            ItemRow = row;

            //can't pass enum or string as type parameter, could use string as instantiation parameter but meh, we'll sell can be easily changed
            //I just like the look up ChangeControl<decimal, IInventoryColumnEnum.Quantity>
            ColumnValue = new DualValue<T>((dynamic)ItemRow[ColumnName]);

            InitializeComponent();



            Loaded += QuantityChangeControl_Loaded;
        }

        private void QuantityChangeControl_Loaded(object sender, RoutedEventArgs e)
        {
            //move control to mouse position

            TranslateTransform trans = new TranslateTransform()
            {
                X = Mouse.GetPosition(grid).X,
                Y = Mouse.GetPosition(grid).Y
            };

            RenderTransform = trans;
            //end move control to mouse position
            Focus();
            Keyboard.Focus(inputAbsBox);
        }

        private void inputBoxes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var value = ItemRow[ColumnName];


                UpdateValue(ColumnValue.Absolute);

                var parent = Application.Current.Windows[0] as MainWindow;

                parent.DropClickMenuIfActive();
            }
        }

        private void UpdateValue(T value)
        {
            if (Equals(ItemRow[ColumnName], value))
                return;

            ItemRow[ColumnName] = value;

            inventory.UpdateRow(ItemRow.liId, ItemRow.itemName, ItemRow.Price, ItemRow.Quantity, ItemRow.Unit, ItemRow.CategoryId);
        }
    }
}
