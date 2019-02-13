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
    public class AssetViewModel : CRUDBase<Asset>, ICRUDViewModel
    {
        public AssetViewModel()
        {
            Name = "Asset";
            ViewItems = CollectionViewSource.GetDefaultView(DataAccess.Assets);
            DtGridProperties = (string)Properties.Settings.Default["AssetDtGridProperties"];
        }

        public override void SaveProperties(Dictionary<string, string> columnDictionary)
        {
            base.SaveProperties(columnDictionary);
            Properties.Settings.Default["AssetDtGridProperties"] = DtGridProperties;
            Properties.Settings.Default.Save();
        }

        protected async override void GetData()
        {
            DataGridSpinnerState = SpinnerState.Loading;
            DataGridState = ViewModeType.Busy;
            await Task.Delay(2000);
            await DataAccess.GetAllAssets();
            ClearFilter();
            DataGridState = ViewModeType.Default;
        }

        protected override void CreateNew()
        {
            NewItem = null;
            Asset newCus = new Asset { Name = "" };
            NewItem = newCus;
        }

        protected async override void Add()
        {
            CRUDType = CRUDType.Adding;
            CRUDState = CRUDCardState.Busy;
            await Task.Delay(2000);
            await DataAccess.AddAsset(NewItem);
            SelectedItem = NewItem;
            CRUDState = CRUDCardState.Default;
        }


        protected override void Edit()
        {
            var newAsset = new Asset();
            newAsset.AssetCategory = new AssetCategory();
            DataAccess.CopyProperties(typeof(Asset), newAsset, SelectedItem);
            InEditItem = newAsset;
        }

        protected async override void Save()
        {
            CRUDType = CRUDType.Saving;
            CRUDState = CRUDCardState.Busy;
            await Task.Delay(2000);

            await DataAccess.SaveAsset(InEditItem, SelectedItem);

            var temp = SelectedItem;                                        // Update data of SelectedItem in View, REQUIRED
            DataAccess.Assets[currentIndex] = new Asset();            // NOT DELETE - IMPORTANT
            DataAccess.Assets[currentIndex] = temp;
            SelectedItem = temp;

            CRUDState = CRUDCardState.Default;
        }

        protected async override void Delete()
        {
            CRUDType = CRUDType.Deleting;
            CRUDState = CRUDCardState.Busy;
            await Task.Delay(2000);

            await DataAccess.DeleteAsset(SelectedItem);
            SelectedItem = null;
            ReFocusRow(DataAccess.Assets.Count);
            CRUDState = CRUDCardState.Default;
        }

        protected async override void Duplicate()
        {
            CRUDType = CRUDType.Copying;
            CRUDState = CRUDCardState.Busy;
            Asset newItem = new Asset();
            newItem.AssetCategory = new AssetCategory();
            DataAccess.CopyProperties(typeof(Asset), newItem, SelectedItem);
            newItem.Id = 0;
            await DataAccess.AddAsset(newItem);
            SelectedIndex = DataAccess.Assets.Count - 1;
            CRUDState = CRUDCardState.Default;
        }

        public async override void DeleteMany(IList items)
        {
            CRUDType = CRUDType.Deleting;
            CRUDState = CRUDCardState.Busy;
            await DataAccess.DeleteManyAssets(items);
            SelectedItem = null;
            ReFocusRow(DataAccess.Assets.Count);
            CRUDState = CRUDCardState.Default;
        }

        #region Filter Asset

        protected async override void FilterData()
        {
            SearchKeyword.Trim();
            DataGridSpinnerState = SpinnerState.Searching;
            DataGridState = ViewModeType.Busy;
            await Task.Delay(400);
            switch (SearchProperty.HeaderName)
            {
                case "Id":
                    ViewItems.Filter = AssetIdFilter;
                    break;
                case "品名":
                    ViewItems.Filter = AssetNameFilter;
                    break;
                case "Tel":
                    ViewItems.Filter = AssetTelFilter;
                    break;
                case "IDCard Number":
                    ViewItems.Filter = AssetIDCardNumberFilter;
                    break;
                case "Address":
                    ViewItems.Filter = AssetAddressFilter;
                    break;
                case "Relationship":
                    ViewItems.Filter = AssetRelationshipFilter;
                    break;
                case "カテゴリ":
                    ViewItems.Filter = AssetCategoryFilter;
                    break;
                default:
                    break;
            }
            IsFiltered = true;
            DataGridState = ViewModeType.Default;

        }

        private bool AssetIdFilter(object item)
        {
            Asset Asset = item as Asset;
            return Asset.Id.ToString().ToLower().Equals((SearchKeyword.ToLower()));
        }

        private bool AssetNameFilter(object item)
        {
            Asset Asset = item as Asset;
            if (Asset.Name == null) return false;
            return Asset.Name.ToLower().Contains(SearchKeyword.ToLower());
        }

        private bool AssetTelFilter(object item)
        {
            return true;
        }

        private bool AssetIDCardNumberFilter(object item)
        {
            return true;
        }

        private bool AssetAddressFilter(object item)
        {
            return true;
        }

        private bool AssetRelationshipFilter(object item)
        {
            return true;
        }

        private bool AssetCategoryFilter(object item)
        {
            Asset Asset = item as Asset;
            if (Asset.AssetCategory.Name == null) return false;
            return Asset.AssetCategory.Name.ToLower().Contains(SearchKeyword.ToLower());
        }

        #endregion
    }
}
