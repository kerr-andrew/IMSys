using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace IMSys
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        QuantityChangeControl clickMenu = null;
        static MainWindow()
        {
            var adapter = new IMSysDBDataSetTableAdapters.InventoryTableAdapter();
            Application.Current.Properties["inventory"] = adapter;
        }
        IMSysDBDataSetTableAdapters.InventoryTableAdapter inventoryAdapter = Application.Current.Properties["inventory"] as IMSysDBDataSetTableAdapters.InventoryTableAdapter;
        public MainWindow()
        {
            InitializeComponent();
            var window = new IMSys.Andrew.Window1();
            window.Show();
            Inventory.ItemsSource = inventoryAdapter.GetData();
            Inventory.MouseRightButtonUp += Inventory_MouseRightButtonUp;
            Inventory.PreviewMouseLeftButtonDown += Inventory_PreviewMouseLeftButtonDown;
        }

        private void Inventory_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (clickMenu != null || MainGrid.Children.Contains(clickMenu))
            {
                var dgci = GetCurrentCell();
                if (!dgci.HasValue || 
                    (string)dgci.Value.Column.Header == "Stock" || 
                    dgci.Value.Item == null || 
                    clickMenu.ItemRow.liId != dgci.Value.InventoryRow().liId)
                {
                    DropClickMenuIfActive();
                    return;
                }
            }
        }
        internal void DropClickMenuIfActive()
        {
            if (clickMenu != null)
            {
                if (MainGrid.Children.Contains(clickMenu))
                    MainGrid.Children.Remove(clickMenu);
                clickMenu = null;
            }
        }
        private T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null) return null;
            T parent = parentObject as T;
            if (parent != null)
                return parent;
            else
                return FindVisualParent<T>(parentObject);
        }
        private DataGridCellInfo? GetCurrentCell()
        {
            Point pt = Mouse.GetPosition(Inventory);
            DataGridCell cell = null;
            VisualTreeHelper.HitTest(Inventory, null, (res) => {
                DataGridCell temp_cell = FindVisualParent<DataGridCell>(res.VisualHit);
                if (temp_cell != null)
                {
                    cell = temp_cell;
                    return HitTestResultBehavior.Stop;
                }
                else
                    return HitTestResultBehavior.Continue;
            }, new PointHitTestParameters(pt));
            if (cell != null)
            {

                return new DataGridCellInfo(cell);
            }
            else
                return null;
        }

        private void Inventory_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var dgci = GetCurrentCell();
            
            Console.WriteLine("MouseRightButtonUp GCC: {0}", dgci.HasValue ? dgci.Value.Column.Header : "none");
            if (!dgci.HasValue || 
                dgci.Value.Item == null || 
                (clickMenu != null && clickMenu.ItemRow.liId != dgci.Value.InventoryRow().liId))
            {
                DropClickMenuIfActive();
            }
            if (!dgci.HasValue)
                return;
            var cell = dgci.Value;

            switch (cell.Column.Header as string)
            {
                case "Stock":
                    ChangeQuantity(cell);
                    break;
                default:
                    DropClickMenuIfActive();
                    return;
            }

        }
        private void ChangeQuantity(DataGridCellInfo cell)
        {
            var row = cell.InventoryRow();
            var qcc = new QuantityChangeControl(row);

            DropClickMenuIfActive();

            clickMenu = qcc;
            MainGrid.Children.Add(qcc);

        }
    }
}
