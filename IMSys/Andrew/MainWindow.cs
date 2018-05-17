using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using IMSys.Controls;

namespace IMSys
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IChangeControl clickMenu = null;
        protected void InitializeAndrew()
        {
            Inventory.MouseRightButtonUp += Inventory_MouseRightButtonUp;
            Inventory.PreviewMouseLeftButtonDown += Inventory_PreviewMouseLeftButtonDown;
        }

        private void Inventory_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (clickMenu != null || MainGrid.Children.Contains(clickMenu))
            { 
                 DropClickMenuIfActive();
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
        private DataGridCellInfo? GetCurrentCell()
        {
            Point pt = Mouse.GetPosition(Inventory);
            DataGridCell cell = null;
            VisualTreeHelper.HitTest(Inventory, null, (res) => {
                DataGridCell temp_cell = res.VisualHit.FindVisualParent<DataGridCell>();
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
            
            if (dgci.IsActiveChangeControl(clickMenu))
            {
                return;
            }
            DropClickMenuIfActive();

            if (!dgci.HasValue)
                return;
            var cell = dgci.Value;

            switch (cell.Column.Header as string)
            {
                case "Quantity":
                    GenerateChangeControl<int, IInventoryColumnEnum.Quantity>(cell);
                    break;
                case "Price":
                    GenerateChangeControl<decimal, IInventoryColumnEnum.Price>(cell);
                    break;
                default:
                    DropClickMenuIfActive();
                    return;
            }

        }
        private void GenerateChangeControl<T, IIC>(DataGridCellInfo cell)
            where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
            where IIC : IInventoryColumnEnum
        {
            var row = cell.InventoryRow();
            if (row == null)
                return;
            var qcc = new ChangeControl<T, IIC>(row);
            clickMenu = qcc;
            MainGrid.Children.Add(qcc);

        }
    }
}
