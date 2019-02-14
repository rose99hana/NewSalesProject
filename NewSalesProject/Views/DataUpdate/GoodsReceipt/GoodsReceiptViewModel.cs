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
    public class GoodsReceiptViewModel : CRUDBase<GoodsReceipt>, ICRUDViewModel
    {
        public GoodsReceiptViewModel()
        {
            Name = "Good Receipts";
            ViewItems = CollectionViewSource.GetDefaultView(DataAccess.GoodsReceipts);
            ReceiptDetailVM = new ReceiptDetailViewModel(this);
        }

        protected ReceiptDetailViewModel receiptDetailVM;
        public ReceiptDetailViewModel ReceiptDetailVM
        {
            get
            {
                return receiptDetailVM;
            }
            set
            {
                receiptDetailVM = value;
                OnPropertyChanged("ReceiptDetailVM");
            }
        }

        public override void OnSelectedItemChanged()
        {
            if (IsAutoUpdateEnable == true)
            {
                InEditItem = SelectedItem;
                ReceiptDetailVM.OnSelectedParentGoodsReceiptChanged(selectedItem);
            }
        }

        public void ChangeSelectedItemTo(GoodsReceipt para)
        {
            SelectedItem = para;
        }

        //private ProductViewModel productViewModel;

        protected override void CreateNew()
        {
            NewItem = new GoodsReceipt();
            ReceiptDetailVM.OnSelectedParentGoodsReceiptChanged(NewItem);
        }

        protected override void CancelCreateNew()
        {
            NewItem = null;
            ReceiptDetailVM.OnSelectedParentGoodsReceiptChanged(NewItem);
        }

        public void CaculateValue(GoodsReceipt para)
        {
            para.SubTotal = 0m;
            foreach(ReceiptDetail item in para.ReceiptDetails)
            {
                para.SubTotal += item.TaxIncTotal;
            }
            para.TotalFee = para.ShippingFee + para.AdditionalFees;
            if (para.SubTotal != 0m)
                para.Total = para.SubTotal + para.TotalFee - para.DiscountOnReceipt;
            else para.Total = 0;
        }


        public void UpdateItem(GoodsReceipt item)
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
            //await DataAccess.AddProductPrice(NewItem);
            SelectedItem = NewItem;
            CRUDState = CRUDCardState.Default;
        }

    }
}
