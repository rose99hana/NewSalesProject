using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NewSalesProject.Behavior
{
    public static class DataGridBrowsableBehavior
    {
        public static bool GetUseBrowsableAttributeOnColumn(DependencyObject obj)
        {
            return (bool)obj.GetValue(UseBrowsableAttributeOnColumnProperty);
        }

        public static void SetUseBrowsableAttributeOnColumn(DependencyObject obj, bool value)
        {
            obj.SetValue(UseBrowsableAttributeOnColumnProperty, value);
        }

        // Using a DependencyProperty as the backing store for UseBrowsableAttributeOnColumn.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseBrowsableAttributeOnColumnProperty =
            DependencyProperty.RegisterAttached("UseBrowsableAttributeOnColumn", typeof(bool), typeof(DataGridBrowsableBehavior), new UIPropertyMetadata(false, UseBrowsableAttributeOnColumnChanged));

        private static void UseBrowsableAttributeOnColumnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dataGrid = d as DataGrid;
            if (dataGrid != null)
            {
                if ((bool)e.NewValue)
                {
                    dataGrid.AutoGeneratingColumn += DataGridOnAutoGeneratingColumn;
                }
                else
                {
                    dataGrid.AutoGeneratingColumn -= DataGridOnAutoGeneratingColumn;
                }
            }
        }

        private static void DataGridOnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            var propDesc = e.PropertyDescriptor as PropertyDescriptor;
            if (propDesc != null)
            {
                foreach (Attribute att in propDesc.Attributes)
                {
                    var browsableAttribute = att as BrowsableAttribute;
                    if (browsableAttribute != null)
                    {
                        if (!browsableAttribute.Browsable)
                        {
                            e.Cancel = true;
                        }
                    }

                    var displayName = att as DisplayNameAttribute;
                    if (displayName != null)
                    {
                        e.Column.Header = displayName.DisplayName;
                    }
                }
            }
        }
    }
}
