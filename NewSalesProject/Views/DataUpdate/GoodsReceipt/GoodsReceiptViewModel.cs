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
        }

        public GoodsReceiptViewModel(ProductViewModel itemPara)
        {
            Name = "Good Receipts";
            ViewItems = CollectionViewSource.GetDefaultView(DataAccess.GoodsReceipts);
            productViewModel = itemPara;
        }

        private ProductViewModel productViewModel;

        protected override void CreateNew()
        {
            NewItem = new GoodsReceipt();
        }

        protected override void CancelCreateNew()
        {
            NewItem = null;
        }

        public void CaculateValue(GoodsReceipt para)
        {
            para.SubTotal = 0m;
            var receiptDetails = productViewModel.ReceiptDetailVM.ViewItems;
            foreach(ReceiptDetail item in receiptDetails)
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
            //CRUDType = CRUDType.Adding;
            //CRUDState = CRUDCardState.Busy;
            //await DataAccess.AddProductPrice(NewItem);
            //SelectedItem = NewItem;
            //CRUDState = CRUDCardState.Default;
        }

    }
}
