using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Threading;

namespace IMSys
{
    public static class InventoryManagementExtensionMethods
    {
        public static Item InventoryRow(this System.Windows.Controls.DataGridCellInfo cell)
        {
            if (cell.Item == CollectionView.NewItemPlaceholder) return null;
            return cell.Item as Item;
        }
        public static bool IsActiveChangeControl(this DataGridCellInfo? dgci, Controls.IChangeControl qcc)
        {
            return dgci.HasValue &&
                dgci.Value.Item != null &&
                dgci.Value.InventoryRow() != null &&
                (qcc != null && qcc.Item.liId == dgci.Value.InventoryRow().liId) &&
                (string)dgci.Value.Column.Header == qcc.Column;
        }
        public static TContainer GetContainerFromIndex<TContainer>(this ItemsControl itemsControl, int index)
            where TContainer : DependencyObject
        {
            return (TContainer)
              itemsControl.ItemContainerGenerator.ContainerFromIndex(index);
        }

        public static bool IsEditing(this DataGrid dataGrid)
        {
            return dataGrid.GetEditingRow() != null;
        }

        public static DataGridRow GetEditingRow(this DataGrid dataGrid)
        {
            if (dataGrid == null)
                return null;
            var sIndex = dataGrid.SelectedIndex;
            if (sIndex >= 0)
            {
                var selected = dataGrid.GetContainerFromIndex<DataGridRow>(sIndex);
                if (selected.IsEditing) return selected;
            }

            for (int i = 0; i < dataGrid.Items.Count; i++)
            {
                if (i == sIndex) continue;
                var item = dataGrid.GetContainerFromIndex<DataGridRow>(i);
                if (item.IsEditing) return item;
            }

            return null;
        }
    }
    //excerpt from https://stackoverflow.com/a/38005903
    public static class DataGridExtensions
    {
        public static void FastEdit(this DataGrid dataGrid)
        {

            if (String.IsNullOrEmpty(nameof(dataGrid)))
                return;

            dataGrid.PreviewMouseLeftButtonDown += (sender, args) => { FastEdit(args.OriginalSource, args); };
        }

        private static void FastEdit(object source, RoutedEventArgs args)
        {
            var dataGridCell = (source as UIElement)?.FindVisualParent<DataGridCell>();

            if (dataGridCell == null || dataGridCell.IsEditing || dataGridCell.IsReadOnly)
            {
                return;
            }

            var dataGrid = dataGridCell.FindVisualParent<DataGrid>();

            if (dataGrid == null)
            {
                return;
            }

            if (!dataGridCell.IsFocused)
            {
                dataGridCell.Focus();
            }

            if (dataGridCell.Content is CheckBox)
            {
                if (dataGrid.SelectionUnit != DataGridSelectionUnit.FullRow)
                {
                    if (!dataGridCell.IsSelected)
                    {
                        dataGridCell.IsSelected = true;
                    }
                }
                else
                {
                    var dataGridRow = dataGridCell.FindVisualParent<DataGridRow>();

                    if (dataGridRow != null && !dataGridRow.IsSelected)
                    {
                        dataGridRow.IsSelected = true;
                    }
                }
            }
            else
            {
                dataGrid.BeginEdit(args);

                dataGridCell.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => { }));
            }
        }
    }

    public static class UIElementExtensions
    {
        public static T FindVisualParent<T>(this DependencyObject element)
            where T : UIElement
        {
            DependencyObject currentElement = element;

            while (currentElement != null)
            {
                var correctlyTyped = currentElement as T;

                if (correctlyTyped != null)
                {
                    return correctlyTyped;
                }

                currentElement = VisualTreeHelper.GetParent(currentElement);
            }

            return null;
        }
    }
}
