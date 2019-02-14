using NewSalesProject.Enum;
using NewSalesProject.Interfaces;
using NewSalesProject.Model;
using NewSalesProject.Supports;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        }

        public ReceiptDetailViewModel(GoodsReceiptViewModel viewModel)
        {
            Name = "Receipt Detail";
            ViewItems = CollectionViewSource.GetDefaultView(DataAccess.ReceiptDetails);
            goodsReceiptVM = viewModel;

        }

        private GoodsReceiptViewModel goodsReceiptVM;
        private GoodsReceipt parentGoodsReceipt;

        private Product selectedProduct;
        public Product SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                OnPropertyChanged("SelectedProduct");
            }
        }

        public override void OnSelectedItemChanged()        //Not delete, prevent InEditItem Changed
        {
        }

        public void OnSelectedProductChanged(Product para)
        {
            SelectedProduct = para;
        }

        public void OnSelectedParentGoodsReceiptChanged(GoodsReceipt para)
        {
            parentGoodsReceipt = para;
            GetData();
        }

        protected override void GetData()
        {
            DataAccess.ReceiptDetails.Clear();
            if (parentGoodsReceipt != null && parentGoodsReceipt.ReceiptDetails.Count != 0)
            {
                DataAccess.ReceiptDetails = new ObservableCollection<ReceiptDetail>(parentGoodsReceipt.ReceiptDetails);
            }
        }

        protected override void Refresh()
        {
            CancelEdit();
            UpdateItemDetails();
            SelectedItem = null;
        }

        public override void UpdateItemDetails()
        {
            foreach (ReceiptDetail item in ViewItems)
            {
                item.CurrencySymbol = parentGoodsReceipt.CurrencySymbol;
                item.Tax = parentGoodsReceipt.TaxRate;
                CaculateValue(item);
            }

            goodsReceiptVM.UpdateItem(parentGoodsReceipt);
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

        protected override void CreateNew()
        {
            NewItem = new ReceiptDetail();
            NewItem.CurrencySymbol = parentGoodsReceipt.CurrencySymbol;
            NewItem.Tax = parentGoodsReceipt.TaxRate;
        }

        protected async override void Add()
        {
            CRUDType = CRUDType.Adding;
            CRUDState = CRUDCardState.Busy;
            await Task.Delay(150);
            NewItem.Product = selectedProduct;
            CaculateValue(NewItem);
            DataAccess.ReceiptDetails.Add(NewItem);
            parentGoodsReceipt.ReceiptDetails.Add(NewItem);
            goodsReceiptVM.UpdateItem(parentGoodsReceipt);

            SelectedItem = NewItem;
            CRUDState = CRUDCardState.Default;
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
            goodsReceiptVM.UpdateItem(parentGoodsReceipt);
            CRUDState = CRUDCardState.Default;
        }


        protected override void Delete()
        {
            CRUDType = CRUDType.Deleting;
            CRUDState = CRUDCardState.Busy;
            DataAccess.ReceiptDetails.Remove(SelectedItem);
            parentGoodsReceipt.ReceiptDetails.Remove(SelectedItem);
            SelectedItem = null;
            ReFocusRow(DataAccess.ProductPrices.Count);
            CRUDState = CRUDCardState.Default;
        }

        protected override void DeleteAll()
        {
            CRUDType = CRUDType.Deleting;
            CRUDState = CRUDCardState.Busy;
            DataAccess.ReceiptDetails.Clear();
            parentGoodsReceipt.ReceiptDetails.Clear();
            SelectedItem = null;
            CRUDState = CRUDCardState.Default;
        }



        
    }
}
