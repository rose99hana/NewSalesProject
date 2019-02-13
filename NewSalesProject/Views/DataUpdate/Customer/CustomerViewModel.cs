using NewSalesProject.Enum;
using NewSalesProject.Interfaces;
using NewSalesProject.Model;
using NewSalesProject.Supports;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace NewSalesProject.Views
{ 
    public class CustomerViewModel : CRUDBase<Customer>, ICRUDViewModel
    {
        public CustomerViewModel()
        {
            Name = "Customer";
            ViewItems = CollectionViewSource.GetDefaultView(DataAccess.Customers);
            DtGridProperties = (string)Properties.Settings.Default["CustomerDtGridProperties"];
        }

        public override void SaveProperties(Dictionary<string, string> columnDictionary)
        {
            base.SaveProperties(columnDictionary);
            Properties.Settings.Default["CustomerDtGridProperties"] = DtGridProperties;
            Properties.Settings.Default.Save();
        }

        protected async override void GetData()
        {
            DataGridSpinnerState = SpinnerState.Loading;
            DataGridState = ViewModeType.Busy;
            await Task.Delay(150);
            await DataAccess.GetAllCustomers();
            ClearFilter();
            DataGridState = ViewModeType.Default;
        }

        protected override void CreateNew()
        {
            NewItem = null;
            Customer newCus = new Customer { Name = "" };
            NewItem = newCus;
        }

        protected async override void Add()
        {
            CRUDType = CRUDType.Adding;
            CRUDState = CRUDCardState.Busy;
            await DataAccess.AddCustomer(NewItem);
            SelectedItem = NewItem;
            CRUDState = CRUDCardState.Default;
        }


        protected override void Edit()
        {
            var newCustomer = new Customer();
            newCustomer.CustomerRank = new CustomerRank();
            DataAccess.CopyProperties(typeof(Customer), newCustomer, SelectedItem);
            InEditItem = newCustomer;
        }

        protected async override void Save()
        {
            CRUDType = CRUDType.Saving;
            CRUDState = CRUDCardState.Busy;
            await DataAccess.SaveCustomer(InEditItem, SelectedItem);

            var temp = SelectedItem;                                        // Update data of SelectedItem in View, REQUIRED
            DataAccess.Customers[currentIndex] = new Customer();            // NOT DELETE - IMPORTANT
            DataAccess.Customers[currentIndex] = temp;
            SelectedItem = temp;

            CRUDState = CRUDCardState.Default;
        }

        protected async override void Delete()
        {
            CRUDType = CRUDType.Deleting;
            CRUDState = CRUDCardState.Busy;
            await DataAccess.DeleteCustomer(SelectedItem);
            SelectedItem = null;
            ReFocusRow(DataAccess.Customers.Count);
            CRUDState = CRUDCardState.Default;
        }

        protected async override void Duplicate()
        {
            CRUDType = CRUDType.Copying;
            CRUDState = CRUDCardState.Busy;
            Customer newItem = new Customer();
            newItem.CustomerRank = new CustomerRank();
            DataAccess.CopyProperties(typeof(Customer), newItem, SelectedItem);
            newItem.Id = 0;
            await DataAccess.AddCustomer(newItem);
            SelectedIndex = DataAccess.Customers.Count - 1;
            CRUDState = CRUDCardState.Default;
        }

        public async override void DeleteMany(IList items)
        {
            CRUDType = CRUDType.Deleting;
            CRUDState = CRUDCardState.Busy;
            await DataAccess.DeleteManyCustomers(items);
            SelectedItem = null;
            ReFocusRow(DataAccess.Customers.Count);
            CRUDState = CRUDCardState.Default;
        }

        #region Filter Customer

        protected async override void FilterData()
        {
            SearchKeyword.Trim();
            DataGridSpinnerState = SpinnerState.Searching;
            DataGridState = ViewModeType.Busy;
            await Task.Delay(400);
            switch (SearchProperty.HeaderName)
            {
                case "Id":
                    ViewItems.Filter = CustomerIdFilter;
                    break;
                case "Name":
                    ViewItems.Filter = CustomerNameFilter;
                    break;
                case "Tel":
                    ViewItems.Filter = CustomerTelFilter;
                    break;
                case "IDCard Number":
                    ViewItems.Filter = CustomerIDCardNumberFilter;
                    break;
                case "Address":
                    ViewItems.Filter = CustomerAddressFilter;
                    break;
                case "Relationship":
                    ViewItems.Filter = CustomerRelationshipFilter;
                    break;
                case "Customer Rank":
                    ViewItems.Filter = CustomerRankFilter;
                    break;
                default:
                    break;
            }
            IsFiltered = true;
            DataGridState = ViewModeType.Default;

        }

        private bool CustomerIdFilter(object item)
        {
            Customer customer = item as Customer;
            return customer.Id.ToString().ToLower().Equals((SearchKeyword.ToLower()));
        }

        private bool CustomerNameFilter(object item)
        {
            Customer customer = item as Customer;
            if (customer.Name == null) return false;
            return customer.Name.ToLower().Contains(SearchKeyword.ToLower());
        }

        private bool CustomerTelFilter(object item)
        {
            Customer customer = item as Customer;
            if (customer.Tel == null) return false;
            return customer.Tel.ToLower().Contains(SearchKeyword.ToLower());
        }

        private bool CustomerIDCardNumberFilter(object item)
        {
            Customer customer = item as Customer;
            if (customer.Tel == null) return false;
            return customer.IdCardNumber.ToLower().Contains(SearchKeyword.ToLower());
        }

        private bool CustomerAddressFilter(object item)
        {
            bool x = false, y = false;
            Customer customer = item as Customer;
            if (customer.Address1 != null)
            {
                x = customer.Address1.ToLower().Contains(SearchKeyword.ToLower());
            }
            if (customer.Address2 != null)
            {
                y = customer.Address2.ToLower().Contains(SearchKeyword.ToLower());
            }
            return x||y;
        }

        private bool CustomerRelationshipFilter(object item)
        {
            Customer customer = item as Customer;
            if (customer.Relationship == null) return false;
            return customer.Relationship.ToLower().Contains(SearchKeyword.ToLower());
        }

        private bool CustomerRankFilter(object item)
        {
            Customer customer = item as Customer;
            if (customer.CustomerRank.Name == null) return false;
            return customer.CustomerRank.Name.ToLower().Contains(SearchKeyword.ToLower());
        }

        #endregion
    }
}
