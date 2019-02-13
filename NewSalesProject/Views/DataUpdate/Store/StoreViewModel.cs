using NewSalesProject.Enum;
using NewSalesProject.Interfaces;
using NewSalesProject.Model;
using NewSalesProject.Supports;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NewSalesProject.Views
{
    public class StoreViewModel : CRUDBase<Store>, ICRUDViewModel
    {
        public StoreViewModel()
        {
            Name = "Store";
            ViewItems = CollectionViewSource.GetDefaultView(DataAccess.Stores);
            DtGridProperties = Properties.Settings.Default.StoreDtGridProperties;
        }

        public override void SaveProperties(Dictionary<string, string> columnDictionary)
        {
            base.SaveProperties(columnDictionary);
            Properties.Settings.Default["StoreDtGridProperties"] = DtGridProperties;
            Properties.Settings.Default.Save();
        }

        protected async override void GetData()
        {
            DataGridSpinnerState = SpinnerState.Loading;
            DataGridState = ViewModeType.Busy;
            await Task.Delay(150);
            await DataAccess.GetAllStores();
            ClearFilter();
            DataGridState = ViewModeType.Default;
        }

        protected override void CreateNew()
        {
            this.NewItem = null;
            Store newStore = new Store { Name = "" };
            this.NewItem = newStore;
        }

        protected async override void Add()
        {
            CRUDType = CRUDType.Adding;
            CRUDState = CRUDCardState.Busy;
            await DataAccess.AddStore(NewItem);
            SelectedItem = NewItem;
            CRUDState = CRUDCardState.Default;
        }


        protected override void Edit()
        {
            var newStore = new Store();
            DataAccess.CopyProperties(typeof(Store), newStore, SelectedItem);
            InEditItem = newStore;
        }

        protected async override void Save()
        {
            CRUDType = CRUDType.Saving;
            CRUDState = CRUDCardState.Busy;
            await DataAccess.SaveStore(InEditItem, SelectedItem);

            var temp = SelectedItem;                                        // Update data of SelectedItem in View, REQUIRED
            DataAccess.Stores[currentIndex] = new Store();            // NOT DELETE - IMPORTANT
            DataAccess.Stores[currentIndex] = temp;
            SelectedItem = temp;

            CRUDState = CRUDCardState.Default;
        }

        protected async override void Delete()
        {
            CRUDType = CRUDType.Deleting;
            CRUDState = CRUDCardState.Busy;
            await DataAccess.DeleteStore(SelectedItem);
            SelectedItem = null;
            ReFocusRow(DataAccess.Stores.Count);
            CRUDState = CRUDCardState.Default;
        }

        protected async override void Duplicate()
        {
            CRUDType = CRUDType.Copying;
            CRUDState = CRUDCardState.Busy;
            Store newItem = new Store();
            DataAccess.CopyProperties(typeof(Store), newItem, SelectedItem);
            newItem.Id = 0;
            newItem.ProductPrices = null;
            await DataAccess.AddStore(newItem);
            SelectedIndex = DataAccess.Stores.Count - 1;
            CRUDState = CRUDCardState.Default;
        }

        public async override void DeleteMany(IList items)
        {
            CRUDType = CRUDType.Deleting;
            CRUDState = CRUDCardState.Busy;
            await DataAccess.DeleteManyStores(items);
            SelectedItem = null;
            ReFocusRow(DataAccess.Stores.Count);
            CRUDState = CRUDCardState.Default;
        }

        #region Filter Store

        protected async override void FilterData()
        {
            SearchKeyword.Trim();
            DataGridSpinnerState = SpinnerState.Searching;
            DataGridState = ViewModeType.Busy;
            await Task.Delay(500);
            switch (SearchProperty.HeaderName)
            {
                case "Id":
                    ViewItems.Filter = StoreIdFilter;
                    break;
                case "Name":
                    ViewItems.Filter = StoreNameFilter;
                    break;
                case "Address":
                    ViewItems.Filter = StoreAddressFilter;
                    break;
                default:
                    break;
            }
            IsFiltered = true;
            DataGridState = ViewModeType.Default;

        }

        private bool StoreIdFilter(object item)
        {
            Store Store = item as Store;
            return Store.Id.ToString().ToLower().Equals((SearchKeyword.ToLower()));
        }

        private bool StoreNameFilter(object item)
        {
            Store Store = item as Store;
            if (Store.Name == null) return false;
            return Store.Name.ToLower().Contains(SearchKeyword.ToLower());
        }

        private bool StoreAddressFilter(object item)
        {
            Store Store = item as Store;
            if (Store.Address == null) return false;
            return Store.Address.ToLower().Contains(SearchKeyword.ToLower());
        }

        #endregion
    }
}
