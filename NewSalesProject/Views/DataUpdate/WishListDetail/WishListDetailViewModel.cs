using NewSalesProject.Enum;
using NewSalesProject.Interfaces;
using NewSalesProject.Model;
using NewSalesProject.Supports;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NewSalesProject.Views
{
    public class WishListDetailViewModel : CRUDBase<WishListDetail>, ICRUDViewModel
    {
        public WishListDetailViewModel()
        {
            Name = "Receipt Detail";
            ViewItems = CollectionViewSource.GetDefaultView(DataAccess.WishListDetails);
            DataAccess.WishListDetails.Add(new WishListDetail());
        }

        public WishListDetailViewModel(WishListViewModel viewModel)
        {
            Name = "Receipt Detail";
            ViewItems = CollectionViewSource.GetDefaultView(DataAccess.WishListDetails);
            WishListVM = viewModel;

        }

        private WishListViewModel WishListVM;
        private WishList parentWishList;

        //private Product selectedProduct;
        //public Product SelectedProduct
        //{
        //    get { return selectedProduct; }
        //    set
        //    {
        //        selectedProduct = value;
        //        OnPropertyChanged("SelectedProduct");
        //    }
        //}

        public override void OnSelectedItemChanged()        //Not delete, prevent InEditItem Changed
        {
        }

        //public void OnSelectedProductChanged(Product para)
        //{
        //    SelectedProduct = para;
        //}

        public void OnSelectedParentWishListChanged(WishList para)
        {
            parentWishList = para;
            GetData();
        }

        protected override void GetData()
        {
            DataAccess.WishListDetails.Clear();
            if (parentWishList != null && parentWishList.WishListDetails.Count != 0)
            {
                DataAccess.WishListDetails = new ObservableCollection<WishListDetail>(parentWishList.WishListDetails);
            }
        }

        protected override void Refresh()
        {
            //CancelEdit();
            //UpdateItemDetails();
            SelectedItem = null;
        }

        public override void UpdateItemDetails()
        {
            //foreach (WishListDetail item in ViewItems)
            //{
            //    item.CurrencySymbol = parentWishList.CurrencySymbol;
            //    item.Tax = parentWishList.TaxRate;
            //    CaculateValue(item);
            //}

            //WishListVM.UpdateItem(parentWishList);
        }

        public void CaculateValue(WishListDetail para)
        {
            //if (para.IsTaxIncluding == false)
            //{
            //    para.SubTotal = para.Price * para.Quantity;
            //    para.DiscountAmount = para.SubTotal * para.Discount / 100;
            //    para.CouponAmount = para.Coupon * para.Quantity;
            //    para.TotalDiscount = para.DiscountAmount + para.CouponAmount;
            //    para.Total = para.SubTotal - para.TotalDiscount;
            //    para.TaxIncTotal = para.Total * (1 + para.Tax / 100);
            //    para.TaxAmount = para.TaxIncTotal - para.Total;

            //}
            //else
            //{
            //    para.SubTotal = para.Price * para.Quantity;
            //    para.DiscountAmount = para.SubTotal * para.Discount / 100;
            //    para.CouponAmount = para.Coupon * para.Quantity;
            //    para.TotalDiscount = para.DiscountAmount + para.CouponAmount;
            //    para.TaxIncTotal = para.SubTotal - para.TotalDiscount;
            //}
        }

        private void UpdateItem(WishListDetail item)
        {
            CaculateValue(item);
            item.RaisePropertyChanged("Price");
            item.RaisePropertyChanged("Discount");
            item.RaisePropertyChanged("Coupon");
            item.RaisePropertyChanged("Quantity");
            item.RaisePropertyChanged("IsTaxIncluding");
            item.RaisePropertyChanged("IsTaxIncluding");
        }

        protected override void CreateNew()
        {
            NewItem = new WishListDetail();
        }

        protected async override void Add()
        {
            CRUDType = CRUDType.Adding;
            CRUDState = CRUDCardState.Busy;
            await Task.Delay(150);
            //NewItem.Product = selectedProduct;
            CaculateValue(NewItem);
            DataAccess.WishListDetails.Add(NewItem);
            parentWishList.WishListDetails.Add(NewItem);
            WishListVM.UpdateItem(parentWishList);

            SelectedItem = NewItem;
            CRUDState = CRUDCardState.Default;
        }

        protected override void Edit()
        {
            InEditItem = new WishListDetail();
            DataAccess.CopyProperties(typeof(WishListDetail), InEditItem, SelectedItem);
            SelectedItem.IsEditable = true;
        }

        public override void CancelEdit()
        {
            if (selectedItem == null) return;
            if (selectedItem.IsEditable == true)
            {
                DataAccess.CopyProperties(typeof(WishListDetail), SelectedItem, InEditItem);
                UpdateItem(SelectedItem);
            }

        }

        public void CancelEditFromView()
        {
            if (selectedItemBeforeChange != null && selectedItemBeforeChange.IsEditable == true)
            {
                DataAccess.CopyProperties(typeof(WishListDetail), selectedItemBeforeChange, InEditItem);
                UpdateItem(selectedItemBeforeChange);
            }
        }

        protected override void Save()
        {
            CRUDType = CRUDType.Saving;
            CRUDState = CRUDCardState.Busy;
            SelectedItem.IsEditable = false;
            UpdateItem(SelectedItem);
            WishListVM.UpdateItem(parentWishList);
            CRUDState = CRUDCardState.Default;
        }


        protected override void Delete()
        {
            CRUDType = CRUDType.Deleting;
            CRUDState = CRUDCardState.Busy;
            DataAccess.WishListDetails.Remove(SelectedItem);
            //parentWishList.WishListDetails.Remove(SelectedItem);
            SelectedItem = null;
            CRUDState = CRUDCardState.Default;
        }

        protected override void DeleteAll()
        {
            CRUDType = CRUDType.Deleting;
            CRUDState = CRUDCardState.Busy;
            DataAccess.WishListDetails.Clear();
            parentWishList.WishListDetails.Clear();
            SelectedItem = null;
            CRUDState = CRUDCardState.Default;
        }
    }
}
