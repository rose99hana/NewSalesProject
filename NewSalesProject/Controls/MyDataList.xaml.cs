using NewSalesProject.Enum;
using NewSalesProject.Interfaces;
using NewSalesProject.Model;
using NewSalesProject.Supports;
using NewSalesProject.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

namespace NewSalesProject.Controls
{
    /// <summary>
    /// Interaction logic for DataGrid.xaml
    /// </summary>
    public partial class MyDataList : UserControl
    {
        public MyDataList()
        {
            InitializeComponent();

            Binding myBinding = new Binding();
            myBinding.Source = ButtonMenuContentItemList;
            Option.ContextMenu.SetBinding(ContextMenu.ItemsSourceProperty, myBinding);
        }

        DataGridColumn lastColumn;
        double totalWidthOfColumnsDtGrid;
        Dictionary<string, string> columnDictionary = new Dictionary<string, string>();
        public ObservableCollection<DataGridColumnPropertyItem> ButtonMenuContentItemList { get; set; } = new ObservableCollection<DataGridColumnPropertyItem>();
        public ObservableCollection<DataGridColumnPropertyItem> SearchPropertyItemList { get; set; } = new ObservableCollection<DataGridColumnPropertyItem>();



        #region Dependency Properties

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(MyDataList), new PropertyMetadata("Items List"));


        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register("SelectedIndex", typeof(int), typeof(MyDataList), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));



        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(MyDataList), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));



        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(MyDataList), new PropertyMetadata(null));




        public ViewModeType DataGridState
        {
            get { return (ViewModeType)GetValue(DataGridStateProperty); }
            set { SetValue(DataGridStateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataGridState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataGridStateProperty =
            DependencyProperty.Register("DataGridState", typeof(ViewModeType), typeof(MyDataList), new FrameworkPropertyMetadata(ViewModeType.Default, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));



        public SpinnerState DataGridSpinnerState
        {
            get { return (SpinnerState)GetValue(DataGridSpinnerStateProperty); }
            set { SetValue(DataGridSpinnerStateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataGridSpinnerState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataGridSpinnerStateProperty =
            DependencyProperty.Register("DataGridSpinnerState", typeof(SpinnerState), typeof(MyDataList), new FrameworkPropertyMetadata(SpinnerState.Loading, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));




        public string SearchKeyWord
        {
            get { return (string)GetValue(SearchKeyWordProperty); }
            set { SetValue(SearchKeyWordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SearchKeyWord.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SearchKeyWordProperty =
            DependencyProperty.Register("SearchKeyWord", typeof(string), typeof(MyDataList), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));



        public bool IsSearchPropertyVisible
        {
            get { return (bool)GetValue(IsSearchPropertyVisibleProperty); }
            set { SetValue(IsSearchPropertyVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsSearchPropertyVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSearchPropertyVisibleProperty =
            DependencyProperty.Register("IsSearchPropertyVisible", typeof(bool), typeof(MyDataList), new PropertyMetadata(true));



        public object SearchProperty
        {
            get { return (object)GetValue(SearchPropertyProperty); }
            set { SetValue(SearchPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SearchProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SearchPropertyProperty =
            DependencyProperty.Register("SearchProperty", typeof(object), typeof(MyDataList), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));



        public bool IsAutoUpdateEnable
        {
            get { return (bool)GetValue(IsAutoUpdateEnableProperty); }
            set { SetValue(IsAutoUpdateEnableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAutoUpdateEnable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAutoUpdateEnableProperty =
            DependencyProperty.Register("IsAutoUpdateEnable", typeof(bool), typeof(MyDataList), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));




        public bool IsFiltered
        {
            get { return (bool)GetValue(IsFilteredProperty); }
            set { SetValue(IsFilteredProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsFiltered.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsFilteredProperty =
            DependencyProperty.Register("IsFiltered", typeof(bool), typeof(MyDataList), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        #endregion



        #region Command

        public ICommand RefreshCommand
        {
            get { return (ICommand)GetValue(RefreshCommandProperty); }
            set { SetValue(RefreshCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RefreshCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RefreshCommandProperty =
            DependencyProperty.Register("RefreshCommand", typeof(ICommand), typeof(MyDataList), new UIPropertyMetadata(null));



        public ICommand AddCommand
        {
            get { return (ICommand)GetValue(AddCommandProperty); }
            set { SetValue(AddCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AddCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AddCommandProperty =
            DependencyProperty.Register("AddCommand", typeof(ICommand), typeof(MyDataList), new PropertyMetadata(null));



        public ICommand DeleteCommand
        {
            get { return (ICommand)GetValue(DeleteCommandProperty); }
            set { SetValue(DeleteCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DeleteCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DeleteCommandProperty =
            DependencyProperty.Register("DeleteCommand", typeof(ICommand), typeof(MyDataList), new PropertyMetadata(null));


        public ICommand FilterCommand
        {
            get { return (ICommand)GetValue(FilterCommandProperty); }
            set { SetValue(FilterCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FilterCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterCommandProperty =
            DependencyProperty.Register("FilterCommand", typeof(ICommand), typeof(MyDataList), new PropertyMetadata(null));



        public ICommand ClearFilterCommand
        {
            get { return (ICommand)GetValue(ClearFilterCommandProperty); }
            set { SetValue(ClearFilterCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ClearFilterCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClearFilterCommandProperty =
            DependencyProperty.Register("ClearFilterCommand", typeof(ICommand), typeof(MyDataList), new PropertyMetadata(null));



        public ICommand UpdateDetailsCommand
        {
            get { return (ICommand)GetValue(UpdateDetailsCommandProperty); }
            set { SetValue(UpdateDetailsCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UpdateDetaisCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UpdateDetailsCommandProperty =
            DependencyProperty.Register("UpdateDetailsCommand", typeof(ICommand), typeof(MyDataList), new PropertyMetadata(null));





        public ICommand DuplicateItemCommand
        {
            get { return (ICommand)GetValue(DuplicateItemCommandProperty); }
            set { SetValue(DuplicateItemCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DuplicateItemCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DuplicateItemCommandProperty =
            DependencyProperty.Register("DuplicateItemCommand", typeof(ICommand), typeof(MyDataList), new PropertyMetadata(null));



        #endregion



        #region Events

        private void Option_Click(object sender, RoutedEventArgs e)
        {
            var mouseRightClickEvent = new MouseButtonEventArgs(Mouse.PrimaryDevice, Environment.TickCount, MouseButton.Right)
            {
                RoutedEvent = Mouse.MouseUpEvent,
                Source = sender,
            };
            InputManager.Current.ProcessInput(mouseRightClickEvent);
        }

        private async void DtGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (columnDictionary.Count == 0 && DataContext != null)
            {
                var ddd = (DataContext as ICRUDViewModel).DtGridProperties;
                var items = (DataContext as ICRUDViewModel).DtGridProperties.Split(';');
                foreach (string item in items)
                {
                    string[] keyValue = item.Split('=');
                    if (keyValue[0] != "")
                        columnDictionary.Add(keyValue[0], keyValue[1]);
                }
            }

            foreach (var item in columnDictionary)
            {
                if (item.Value == "false")
                {
                    try
                    {
                        var column = ButtonMenuContentItemList.Where(p => p.HeaderName == item.Key).Single();
                        column.IsChecked = false;
                        DtGrid.Columns.Where(p => p.Header.ToString() == column.HeaderName).Single().Visibility = Visibility.Collapsed;
                    }
                    catch { }
                }
            }

            await SetDtGridLastColumnWidth();
        }

        private async Task SetDtGridLastColumnWidth()
        {
            totalWidthOfColumnsDtGrid = 0;
            foreach (var column in DtGrid.Columns)
            {
                if (column.Visibility == Visibility.Visible)
                {
                    column.Width = new DataGridLength(0, DataGridLengthUnitType.Auto);
                }
            }
            DtGrid.Refresh();
            await Task.Delay(10);       //for render recaculate width of column
            foreach (var column in DtGrid.Columns)
            {
                if (column.Visibility == Visibility.Visible)
                {
                    lastColumn = column;
                    totalWidthOfColumnsDtGrid += column.ActualWidth;
                }
            }

            if (lastColumn != null)
                if (totalWidthOfColumnsDtGrid < DtGrid.ActualWidth)
                    lastColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            DtGrid.Refresh();
            await Task.Delay(10);       //for render recaculate width of column
            if (lastColumn != null) lastColumn.Width = lastColumn.ActualWidth;
        }

        private async void Spinner_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(Spinner.Visibility == Visibility.Collapsed)
            {
                await SetDtGridLastColumnWidth();
            }
        }

        private void DtGrid_Unloaded(object sender, RoutedEventArgs e)
        {
            if(DataContext != null)
                (DataContext as ICRUDViewModel).SaveProperties(columnDictionary);
        }

        private void DtGrid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(columnDictionary.Count != 0)
            {
                if(e.OldValue != null)
                    (e.OldValue as ICRUDViewModel).SaveProperties(columnDictionary);
            }
        }


        private void DataGrid_AutoGeneratedColumns(object sender, EventArgs e)
        {
            ButtonMenuContentItemList.Clear();
            SearchPropertyItemList.Clear();
            var context = (DataContext as ICRUDViewModel);

            if (context.DtGridProperties == "Default")
            {
                context.DtGridProperties = "";
                foreach (var item in DtGrid.Columns)
                {
                    context.DtGridProperties += item.Header + "=true;";
                }

                if (DtGrid.DataContext is CustomerViewModel)
                    context.DtGridProperties += "Customer Rank=true;";
                if (DtGrid.DataContext is CustomerRankViewModel)
                    context.DtGridProperties += "Number of Customers=true;";
            }

            ButtonMenuContentItemList.Add(new DataGridColumnPropertyItem { HeaderName = "Deselect All", IsChecked = true });
            foreach (DataGridColumn d in DtGrid.Columns)
            {
                ButtonMenuContentItemList.Add(new DataGridColumnPropertyItem { HeaderName = d.Header.ToString(), IsChecked = true });
                SearchPropertyItemList.Add(new DataGridColumnPropertyItem { HeaderName = d.Header.ToString(), IsChecked = true });

            }


            if (DtGrid.DataContext is CustomerViewModel)
            {
                var col = new DataGridTextColumn();
                col.Header = "Customer Rank";
                col.Binding = new Binding("CustomerRank.Name");
                DtGrid.Columns.Add(col);

                var col2 = new DataGridTextColumn();
                col2.Header = "CustomerRankID";
                col2.Binding = new Binding("CustomerRankID");
                DtGrid.Columns.Add(col2);

                var col1 = new DataGridTextColumn();
                col1.Header = "Rank ID";
                col1.Binding = new Binding("CustomerRank.Id");
                DtGrid.Columns.Add(col1);

                ButtonMenuContentItemList.Add(new DataGridColumnPropertyItem { HeaderName = "Customer Rank", IsChecked = true });
                ButtonMenuContentItemList.Add(new DataGridColumnPropertyItem { HeaderName = "Rank ID", IsChecked = true });
                ButtonMenuContentItemList.Add(new DataGridColumnPropertyItem { HeaderName = "CustomerRankID", IsChecked = true });
                SearchPropertyItemList.Add(new DataGridColumnPropertyItem { HeaderName = "Customer Rank", IsChecked = true });

                SearchPropertyItemList.RemoveAt(4); //Remove address1
                SearchPropertyItemList.RemoveAt(4); //Remove address2
                SearchPropertyItemList.Insert(4, new DataGridColumnPropertyItem { HeaderName = "Address" });
            }
            
            if(DtGrid.DataContext is CustomerRankViewModel)
            {
                var col = new DataGridTextColumn();
                col.Header = "Number of Customers";
                col.Binding = new Binding("Customers.Count");
                DtGrid.Columns.Add(col);

                ButtonMenuContentItemList.Add(new DataGridColumnPropertyItem { HeaderName = "Number of Customers", IsChecked = true });
                SearchPropertyItemList.Add(new DataGridColumnPropertyItem { HeaderName = "Number of Customers", IsChecked = true });
            }

            if (DtGrid.DataContext is AssetViewModel)
            {
                var col = new DataGridTextColumn();
                col.Header = "責任部門";
                col.Binding = new Binding("Department.Name");
                DtGrid.Columns.Add(col);
                ButtonMenuContentItemList.Add(new DataGridColumnPropertyItem { HeaderName = "責任部門", IsChecked = true });
                SearchPropertyItemList.Add(new DataGridColumnPropertyItem { HeaderName = "責任部門", IsChecked = true });

                var col1 = new DataGridTextColumn();
                col1.Header = "設置場所";
                col1.Binding = new Binding("InstallationLocation.Name");
                DtGrid.Columns.Add(col1);
                ButtonMenuContentItemList.Add(new DataGridColumnPropertyItem { HeaderName = "設置場所", IsChecked = true });
                SearchPropertyItemList.Add(new DataGridColumnPropertyItem { HeaderName = "設置場所", IsChecked = true });

                var col2 = new DataGridTextColumn();
                col2.Header = "カテゴリ";
                col2.Binding = new Binding("AssetCategory.Name");
                DtGrid.Columns.Add(col2);
                ButtonMenuContentItemList.Add(new DataGridColumnPropertyItem { HeaderName = "カテゴリ", IsChecked = true });
                SearchPropertyItemList.Add(new DataGridColumnPropertyItem { HeaderName = "カテゴリ", IsChecked = true });
            }
        }

        private void SearchPropertyCbb_Loaded(object sender, RoutedEventArgs e)
        {
            SearchPropertyCbb.SelectedIndex = 1;
        }

        private async void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var checkbox = sender as CheckBox;
            if (checkbox.Content.ToString() == "Select All")
            {
                foreach (DataGridColumnPropertyItem item in Option.ContextMenu.Items)
                {
                    item.IsChecked = true;
                }
                foreach (DataGridColumn column in DtGrid.Columns)
                {
                    column.Visibility = Visibility.Visible;
                    columnDictionary[column.Header.ToString()] = "true";
                }
                (Option.ContextMenu.Items[0] as DataGridColumnPropertyItem).HeaderName = "Deselect All";
            }
            else
            {
                foreach (DataGridColumnPropertyItem item in ButtonMenuContentItemList)
                {
                    if (checkbox.Content.ToString() == item.HeaderName)
                    {
                        item.IsChecked = true;
                        DtGrid.Columns.Where((p) => p.Header.ToString() == item.HeaderName).SingleOrDefault().Visibility = Visibility.Visible;
                        columnDictionary[item.HeaderName] = "true";
                        break;
                    }
                }
            }
            await SetDtGridLastColumnWidth();
        }

        private async void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var checkbox = sender as CheckBox;
            if (checkbox.Content.ToString() == "Deselect All")
            {
                foreach (DataGridColumnPropertyItem item in Option.ContextMenu.Items)
                {
                    item.IsChecked = false;
                }
                foreach (DataGridColumn column in DtGrid.Columns)
                {
                    column.Visibility = Visibility.Collapsed;
                    columnDictionary[column.Header.ToString()] = "false";
                }
                (Option.ContextMenu.Items[0] as DataGridColumnPropertyItem).HeaderName = "Select All";
            }
            else
            {
                foreach (DataGridColumnPropertyItem item in ButtonMenuContentItemList)
                {
                    if (checkbox.Content.ToString() == item.HeaderName)
                    {
                        item.IsChecked = false;
                        DtGrid.Columns.Where((p) => p.Header.ToString() == item.HeaderName).SingleOrDefault().Visibility = Visibility.Collapsed;
                        columnDictionary[item.HeaderName] = "false";
                        break;
                    }
                }
            }
            await SetDtGridLastColumnWidth();
        }

        private void SearchKeyworkTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && SearchButton.IsEnabled == true)
            {
                SearchButton.Command.Execute("Filter");
            }
        }

        private void DeleteManyButton_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as ICRUDViewModel).DeleteMany(DtGrid.SelectedItems);
        }

        private void DtGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DtGrid.SelectedItems.Count > 1 || DtGrid.SelectedItems.Count == 0)
            {
                DeleteButton.IsEnabled = false;
                DuplicateButton.IsEnabled = false;
            }
            else
            {
                DeleteButton.IsEnabled = true;
                DuplicateButton.IsEnabled = true;
            }


            if (AddButton.IsEnabled == false)
                AddButton.IsEnabled = true;

        }


        private void DtGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(GetDetailsButton != null)
                GetDetailsButton.Command.Execute(GetDetailsButton.CommandParameter);
        }

        private void AutoUpdateCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            DtGrid.RemoveHandler(DataGridRow.MouseDoubleClickEvent, new MouseButtonEventHandler(DtGridRow_MouseDoubleClick));
        }

        private void AutoUpdateCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            DtGrid.AddHandler(DataGridRow.MouseDoubleClickEvent, new MouseButtonEventHandler(DtGridRow_MouseDoubleClick));
        }

        #endregion



        #region Drag and Drop GetDetailsButton

        Point anchorPoint;
        Point currentPoint;
        bool isInDrag = false;
        private TranslateTransform transform = new TranslateTransform();

        private void GetDetailsButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var btn = sender as Button;
            anchorPoint = e.GetPosition(HaveButtonGrid);
            isInDrag = true;
        }

        private void GetDetailsButton_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isInDrag)
            {
                isInDrag = false;
            }
        }

        private void GetDetailsButton_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (isInDrag)
            {
                var btn = sender as Button;
                currentPoint = e.GetPosition(HaveButtonGrid);

                if (currentPoint.X < HaveButtonGrid.RenderSize.Width &&
                    currentPoint.Y < HaveButtonGrid.RenderSize.Height &&
                    currentPoint.X > 0 &&
                    currentPoint.Y > 0)
                {
                    transform.X += (currentPoint.X - anchorPoint.X);
                    transform.Y += (currentPoint.Y - anchorPoint.Y);

                    btn.RenderTransform = transform;
                    anchorPoint = currentPoint;
                }
            }
        }


        #endregion

    }



    
}
