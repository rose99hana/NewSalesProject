using NewSalesProject.Enum;
using NewSalesProject.Interfaces;
using NewSalesProject.Model;
using NewSalesProject.Supports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NewSalesProject.Views
{
    public class WishListViewModel : CRUDBase<WishList>, ICRUDViewModel
    {
        public WishListViewModel()
        {
            Name = "WishLists";
            ViewItems = CollectionViewSource.GetDefaultView(DataAccess.WishLists);
            WishListDetailVM = new WishListDetailViewModel(this);
        }

        protected WishListDetailViewModel wishListDetailVM;
        public WishListDetailViewModel WishListDetailVM
        {
            get
            {
                return wishListDetailVM;
            }
            set
            {
                wishListDetailVM = value;
                OnPropertyChanged("WishListDetailVM");
            }
        }

        public override void OnSelectedItemChanged()
        {
            if (IsAutoUpdateEnable == true)
            {
                InEditItem = SelectedItem;
                WishListDetailVM.OnSelectedParentWishListChanged(selectedItem);
            }
        }

        public void ChangeSelectedItemTo(WishList para)
        {
            SelectedItem = para;
        }

        //private ProductViewModel productViewModel;

        protected override void CreateNew()
        {
            NewItem = new WishList();
            WishListDetailVM.OnSelectedParentWishListChanged(NewItem);
        }

        protected override void CancelCreateNew()
        {
            NewItem = null;
            WishListDetailVM.OnSelectedParentWishListChanged(NewItem);
        }

        public void CaculateValue(WishList para)
        {
            //para.SubTotal = 0m;
            //foreach (WishListDetail item in para.WishListDetails)
            //{
            //    para.SubTotal += item.TaxIncTotal;
            //}
            //para.TotalFee = para.ShippingFee + para.AdditionalFees;
            //if (para.SubTotal != 0m)
            //    para.Total = para.SubTotal + para.TotalFee - para.DiscountOnReceipt;
            //else para.Total = 0;
        }


        public void UpdateItem(WishList item)
        {
            CaculateValue(item);
            item.RaisePropertyChanged("TaxRate");
            item.RaisePropertyChanged("ExchangeRate");
            item.RaisePropertyChanged("DiscountOnReceipt");
            item.RaisePropertyChanged("ShippingFee");
            item.RaisePropertyChanged("AdditionalFees");
            item.RaisePropertyChanged("CurrencySymbol");
        }

        protected async override void Add()
        {
            CRUDType = CRUDType.Adding;
            CRUDState = CRUDCardState.Busy;
            //await DataAccess.AddWishList(NewItem);
            SelectedItem = NewItem;
            CRUDState = CRUDCardState.Default;
        }

    }
}
