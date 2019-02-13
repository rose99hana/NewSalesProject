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
    public class CustomerRankViewModel : CRUDBase<CustomerRank>, ICRUDViewModel
    {
        public CustomerRankViewModel()
        {
            Name = "Customer Rank";
            ViewItems = CollectionViewSource.GetDefaultView(DataAccess.CustomerRanks);
            DtGridProperties = Properties.Settings.Default.CustomerRankDtGridProperties;
        }

        public override void SaveProperties(Dictionary<string, string> columnDictionary)
        {
            base.SaveProperties(columnDictionary);
            Properties.Settings.Default["CustomerRankDtGridProperties"] = DtGridProperties;
            Properties.Settings.Default.Save();
        }

        protected async override void GetData()
        {
            DataGridSpinnerState = SpinnerState.Loading;
            DataGridState = ViewModeType.Busy;
            await Task.Delay(150);
            await DataAccess.GetAllCustomerRanks();
            ClearFilter();
            DataGridState = ViewModeType.Default;
        }

        protected override void CreateNew()
        {
            this.NewItem = null;
            CustomerRank newEntity = new CustomerRank { Name = "" };
            this.NewItem = newEntity;
        }

        protected async override void Add()
        {
            CRUDType = CRUDType.Adding;
            CRUDState = CRUDCardState.Busy;
            await DataAccess.AddCustomerRank(NewItem);
            SelectedItem = NewItem;
            CRUDState = CRUDCardState.Default;
        }


        protected override void Edit()
        {
            var newEntity = new CustomerRank();
            DataAccess.CopyProperties(typeof(CustomerRank), newEntity, SelectedItem);
            InEditItem = newEntity;
        }

        protected async override void Save()
        {
            CRUDType = CRUDType.Saving;
            CRUDState = CRUDCardState.Busy;
            await DataAccess.SaveCustomerRank(InEditItem, SelectedItem);

            var temp = SelectedItem;                                        // Update data of SelectedItem in View, REQUIRED
            DataAccess.CustomerRanks[currentIndex] = new CustomerRank();            // NOT DELETE - IMPORTANT
            DataAccess.CustomerRanks[currentIndex] = temp;
            SelectedItem = temp;

            CRUDState = CRUDCardState.Default;
        }

        protected async override void Delete()
        {
            CRUDType = CRUDType.Deleting;
            CRUDState = CRUDCardState.Busy;
            await DataAccess.DeleteCustomerRank(SelectedItem);
            SelectedItem = null;
            ReFocusRow(DataAccess.CustomerRanks.Count);
            CRUDState = CRUDCardState.Default;
        }

        protected async override void Duplicate()
        {
            CRUDType = CRUDType.Copying;
            CRUDState = CRUDCardState.Busy;
            CustomerRank newEntity = new CustomerRank();
            DataAccess.CopyProperties(typeof(CustomerRank), newEntity, SelectedItem);
            newEntity.Customers = null;
            newEntity.Id = 0;
            await DataAccess.AddCustomerRank(newEntity);
            SelectedIndex = DataAccess.CustomerRanks.Count - 1;
            CRUDState = CRUDCardState.Default;
        }

        public async override void DeleteMany(IList items)
        {
            CRUDType = CRUDType.Deleting;
            CRUDState = CRUDCardState.Busy;
            await DataAccess.DeleteManyCustomerRanks(items);
            SelectedItem = null;
            ReFocusRow(DataAccess.CustomerRanks.Count);
            CRUDState = CRUDCardState.Default;
        }

        #region Filter CustomerRank

        protected async override void FilterData()
        {
            SearchKeyword.Trim();
            DataGridSpinnerState = SpinnerState.Searching;
            DataGridState = ViewModeType.Busy;
            await Task.Delay(500);
            switch (SearchProperty.HeaderName)
            {
                case "Id":
                    ViewItems.Filter = CustomerRankIdFilter;
                    break;
                case "Name":
                    ViewItems.Filter = CustomerRankNameFilter;
                    break;
                default:
                    break;
            }
            IsFiltered = true;
            DataGridState = ViewModeType.Default;

        }

        private bool CustomerRankIdFilter(object item)
        {
            CustomerRank CustomerRank = item as CustomerRank;
            return CustomerRank.Id.ToString().ToLower().Equals((SearchKeyword.ToLower()));
        }

        private bool CustomerRankNameFilter(object item)
        {
            CustomerRank CustomerRank = item as CustomerRank;
            if (CustomerRank.Name == null) return false;
            return CustomerRank.Name.ToLower().Contains(SearchKeyword.ToLower());
        }

        #endregion


    }
}
