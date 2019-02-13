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
    public class ReceiptDetailViewModel : CRUDBase<ReceiptDetail>, ICRUDViewModel
    {
        public ReceiptDetailViewModel()
        {
            Name = "Receipt Detail";
            ViewItems = CollectionViewSource.GetDefaultView(DataAccess.ReceiptDetails);
            var MyCollection = new ObservableCollection<ReceiptDetail>(productViewModel.GoodsReceiptVM.NewItem.ReceiptDetails);
        }

        public ReceiptDetailViewModel(ProductViewModel itemPara)
        {
            Name = "Receipt Detail";
            ViewItems = CollectionViewSource.GetDefaultView(DataAccess.ReceiptDetails);
            productViewModel = itemPara;
        }

        private ProductViewModel productViewModel;

        protected override void Refresh()
        {
            CancelEdit();
            UpdateItemDetails();
            SelectedItem = null;
        }

        protected override void CreateNew()
        {
            NewItem = new ReceiptDetail();
            NewItem.CurrencySymbol = productViewModel.GoodsReceiptVM.NewItem.CurrencySymbol;
            NewItem.Tax = productViewModel.GoodsReceiptVM.NewItem.TaxRate;
        }

        protected async override void Add()
        {
            CRUDType = CRUDType.Adding;
            CRUDState = CRUDCardState.Busy;
            await Task.Delay(150);
            NewItem.Product = productViewModel.SelectedItem;
            CaculateValue(NewItem);
            DataAccess.ReceiptDetails.Add(NewItem);

            productViewModel.GoodsReceiptVM.UpdateItem(productViewModel.GoodsReceiptVM.NewItem);

            SelectedItem = NewItem;
            CRUDState = CRUDCardState.Default;
        }

        protected void Add(ReceiptDetail para)
        {
            CaculateValue(para);
            DataAccess.ReceiptDetails.Add(para);
            productViewModel.GoodsReceiptVM.UpdateItem(productViewModel.GoodsReceiptVM.NewItem);
        }

        public override void UpdateItemDetails()
        {
            foreach (ReceiptDetail item in ViewItems)
            {
                item.CurrencySymbol = productViewModel.GoodsReceiptVM.NewItem.CurrencySymbol;
                item.Tax = productViewModel.GoodsReceiptVM.NewItem.TaxRate;
                CaculateValue(item);
            }

            productViewModel.GoodsReceiptVM.UpdateItem(productViewModel.GoodsReceiptVM.NewItem);
        }

        public void CaculateValue(ReceiptDetail para)
        {
            if(para.IsTaxIncluding == false)
            {
                para.SubTotal = para.Price * para.Quantity;
                para.DiscountAmount = para.SubTotal * para.Discount / 100;
                para.CouponAmount = para.Coupon * para.Quantity;
                para.TotalDiscount = para.DiscountAmount + para.CouponAmount;
                para.Total = para.SubTotal - para.TotalDiscount;
                para.TaxIncTotal = para.Total * (1 + para.Tax / 100);
                para.TaxAmount = para.TaxIncTotal - para.Total;

            }
            else
            {
                para.SubTotal = para.Price * para.Quantity;
                para.DiscountAmount = para.SubTotal * para.Discount / 100;
                para.CouponAmount = para.Coupon * para.Quantity;
                para.TotalDiscount = para.DiscountAmount + para.CouponAmount;
                para.TaxIncTotal = para.SubTotal - para.TotalDiscount;
            }
        }

        public override void OnSelectedItemChanged()        //Not delete, prevent InselectedItem Changed
        {
        }


        protected override void Edit()
        {
            InEditItem = new ReceiptDetail();
            DataAccess.CopyProperties(typeof(ReceiptDetail), InEditItem, SelectedItem);
            SelectedItem.IsEditable = true;
        }

        public override void CancelEdit()
        {
            if (selectedItem == null) return;
            if (selectedItem.IsEditable == true)
            {
                DataAccess.CopyProperties(typeof(ReceiptDetail), SelectedItem, InEditItem);
                UpdateItem(SelectedItem);
            }

        }

        public void CancelEditFromView()
        {
            if (selectedItemBeforeChange != null && selectedItemBeforeChange.IsEditable == true)
            {
                DataAccess.CopyProperties(typeof(ReceiptDetail), selectedItemBeforeChange, InEditItem);
                UpdateItem(selectedItemBeforeChange);
            }
        }

        protected override void Save()
        {
            CRUDType = CRUDType.Saving;
            CRUDState = CRUDCardState.Busy;
            SelectedItem.IsEditable = false;
            UpdateItem(SelectedItem);
            productViewModel.GoodsReceiptVM.UpdateItem(productViewModel.GoodsReceiptVM.NewItem);
            CRUDState = CRUDCardState.Default;
        }


        protected override void Delete()
        {
            CRUDType = CRUDType.Deleting;
            CRUDState = CRUDCardState.Busy;
            DataAccess.ReceiptDetails.Remove(SelectedItem);
            SelectedItem = null;
            ReFocusRow(DataAccess.ProductPrices.Count);
            CRUDState = CRUDCardState.Default;
        }

        protected override void DeleteAll()
        {
            CRUDType = CRUDType.Deleting;
            CRUDState = CRUDCardState.Busy;
            DataAccess.ReceiptDetails.Clear();
            SelectedItem = null;
            CRUDState = CRUDCardState.Default;
        }

        private void UpdateItem(ReceiptDetail item)
        {
            CaculateValue(item);
            item.RaisePropertyChanged("Price");
            item.RaisePropertyChanged("Discount");
            item.RaisePropertyChanged("Coupon");
            item.RaisePropertyChanged("Quantity");
            item.RaisePropertyChanged("IsTaxIncluding");
            item.RaisePropertyChanged("IsTaxIncluding");
        }

        
    }
}
